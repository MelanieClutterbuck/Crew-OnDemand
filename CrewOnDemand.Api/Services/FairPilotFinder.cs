using System;
using System.Collections.Generic;
using System.Linq;
using CrewOnDemand.Api.Data;
using Microsoft.Extensions.Logging;

namespace CrewOnDemand.Api.Services
{
    /// <summary>
    /// fairly distributes flights across crew - uses random allocation to even things out (over time).
    /// distributes flights in the following order:
    /// 1/ find a pilot with no bookings
    /// 2/ if not, find a pilot with no bookings that day
    /// 3/ if not, find a pilot that has bookings that day, but can fit the flight in
    /// 4/ if not, fail to find a pilot ...
    /// </summary>
    public class FairPilotFinder : IPilotFinder
    {
        private List<int> _potentialCrew;
        private List<int> _potentialCrewWithBookings;
        private List<CrewBooking> _potentialCrewMembersBookings;
        private DateTime _departing;
        private readonly ILogger<FairPilotFinder> _logger;

        public FairPilotFinder(ILogger<FairPilotFinder> logger)
        {
            _logger = logger;
        }

        public int SelectPilot(CrewLists crewLists, DateTime departing)
        {
            _potentialCrew = crewLists.IdsOfPotentialCrew;
            _potentialCrewWithBookings = crewLists.IdsOfPotentialCrewWithBookings;
            _potentialCrewMembersBookings = crewLists.PotentialCrewMembersBookings;
            _departing = departing;

            _logger.LogInformation($"Started SelectPilot: departing {departing}");

            var pilotId = FindPilotWithNoBookings();

            if (pilotId > 0)
            {
                _logger.LogInformation("Completed FindPilotWithNoBookings");
                return pilotId;
            }

            pilotId = FindPilotWhoIsFreeToday();

            if (pilotId > 0)
            {
                _logger.LogInformation("Completed FindPilotWhoIsFreeToday");
                return pilotId;
            }

            pilotId = FindPilotWhoIsBusyToday();

            _logger.LogInformation($"Completed SelectPilot with FindPilotWhoIsBusyToday: departing {departing}");

            return pilotId > 0 ? pilotId : 0;
        }

        /// <summary>
        /// 1. find local working crew with no bookings at all
        /// </summary>
        /// <returns></returns>
        private int FindPilotWithNoBookings()
        {
            var notBusyCrew = _potentialCrew.Except(_potentialCrewWithBookings).ToList();

            if (notBusyCrew.Any())
            {
                // randomly select a pilot 
                var randomCrewId = notBusyCrew[new Random().Next(0, notBusyCrew.Count - 1)];
                return randomCrewId;
            }
            else 
            {
                return 0;
            }
        }

        /// <summary>
        /// 2. find local working crew with no bookings that day 
        /// </summary>
        /// <returns></returns>
        private int FindPilotWhoIsFreeToday()
        {
            var quiteBusyCrew = _potentialCrewMembersBookings
                .Where(cb => cb.Departing.Date != _departing.Date).ToList();

            if (quiteBusyCrew.Any())
            {
                // randomly select a pilot 
                var quiteBusyCrewIds = quiteBusyCrew.Select(cb => cb.CrewId).ToList();
                var randomCrewId = quiteBusyCrewIds[new Random().Next(0, quiteBusyCrewIds.Count - 1)];
                return randomCrewId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 3. find local working crew with bookings on that day, but they don't conflict
        /// N.B. the pilot needs to return with an hour to spare, before taking a break
        /// and doing plane care and pre-flight routines etc. before taking off again.
        /// </summary>
        /// <returns></returns>
        private int FindPilotWhoIsBusyToday()
        {
            var busyCrew = _potentialCrewMembersBookings
                .Where(cb => cb.Departing.Year == _departing.Year
                              && cb.Departing.Month == _departing.Month
                              && cb.Departing.Day == _departing.Day
                                && cb.Returning.AddHours(cb.Duration + 1) < _departing).ToList();

            if (busyCrew.Any())
            {
                // randomly select a pilot 
                var busyCrewIds = busyCrew.Select(cb => cb.CrewId).ToList();
                var randomCrewId = busyCrewIds[new Random().Next(0, busyCrewIds.Count - 1)];
                return randomCrewId;
            }
            else
            {
                return 0;
            }
        }
    }
}