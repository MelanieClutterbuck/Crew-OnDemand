using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrewOnDemand.Api.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CrewOnDemand.Api.Services
{
    public class CrewMemberService : ICrewMemberService
    {
        private readonly ILogger<CrewMemberService> _logger;
        private readonly IConfiguration _config;
        private readonly ICrewBookingRepository _crewBookingRepository;
        private readonly ICrewRepository _crewRepository;
        private readonly IPilotFinder _pilotFinder;

        public CrewMemberService(IConfiguration config, ILogger<CrewMemberService> logger,
            ICrewBookingRepository crewBookingRepository, ICrewRepository crewRepository, IPilotFinder pilotFinder)
        {
            _config = config;
            _logger = logger;
            _crewBookingRepository = crewBookingRepository;
            _crewRepository = crewRepository;
            _pilotFinder = pilotFinder;
        }

        public async Task<CrewMember> PilotAvailableFor(DateTime departing, string fromBase)
        {
            _logger.LogInformation($"Started PilotAvailableFor: departing {departing}, fromBase {fromBase}");

            // optimisation of the repos - they currently return All - is not necessary now, but may be in future
            var crewMembers = await _crewRepository.Get();
            var crewBookings = await _crewBookingRepository.Get();
            var localWorkingCrew = crewMembers.Where(c => c.WorkDays.Contains(departing.DayOfWeek.ToString()) && c.Base.Equals(fromBase)).ToList();

            var crewLists = BuildCrewLists(crewMembers.ToList(), crewBookings.ToList(), localWorkingCrew);

            var selectedPilot = _pilotFinder.SelectPilot(crewLists, departing);

            _logger.LogInformation($"Completed PilotAvailableFor: departing {departing}, fromBase {fromBase}");

            return localWorkingCrew.Find(crew => crew.Id == selectedPilot);
        }

        private CrewLists BuildCrewLists(IEnumerable<CrewMember> crewMembers, IReadOnlyCollection<CrewBooking> crewBookings, IEnumerable<CrewMember> localWorkingCrew)
        {
           
            var idsOfPotentialCrew = localWorkingCrew.Select(lw => lw.Id).ToList();

            var crewWithBookings = crewBookings.Select(cb => cb.CrewId).ToList();
            var idsOfPotentialCrewWithBookings = idsOfPotentialCrew.Intersect(crewWithBookings).ToList();

            var potentialCrewMembersBookings =
                from potentialCrewIds in idsOfPotentialCrewWithBookings
                join booking in crewBookings on potentialCrewIds equals booking.CrewId
                select new CrewBooking()
                {
                    CrewId = potentialCrewIds,
                    Departing = booking.Departing,
                    StartLocation = booking.StartLocation,
                    EndLocation = booking.EndLocation
                };

            var crewLists = new CrewLists(idsOfPotentialCrew, idsOfPotentialCrewWithBookings, potentialCrewMembersBookings.ToList());
            return crewLists;
        }
    }
}
