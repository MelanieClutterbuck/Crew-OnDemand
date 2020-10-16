using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrewOnDemand.Api.Bus;
using CrewOnDemand.Api.Bus.Messages;
using CrewOnDemand.Api.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CrewOnDemand.Api.Services
{
    public class CrewBookingService : ICrewBookingService
    {
        private readonly ILogger<CrewBookingService> _logger;
        private readonly IConfiguration _config;
        private readonly ICrewBookingRepository _crewBookingRepository;
        private readonly IMessageBus _messageBus;

        public CrewBookingService(IConfiguration config, ILogger<CrewBookingService> logger,
            ICrewBookingRepository crewBookingRepository, IMessageBus messageBus)
        {
            _logger = logger;
            _config = config;
            _crewBookingRepository = crewBookingRepository;
            _messageBus = messageBus;
        }
        
        public async Task<bool> BookPilot(CrewMember crewMember, CrewBooking crewBooking)
        {
            _logger.LogInformation($"Started BookPilot: CrewId {crewBooking.CrewId}, Departing {crewBooking.Departing}, StartLocation {crewBooking.StartLocation}");

            var pilotBooked = await _crewBookingRepository.Add(crewBooking);

            if (!pilotBooked) return false;

            _logger.LogInformation($"BookPilot: booking saved to repository: CrewId {crewBooking.CrewId}, Departing {crewBooking.Departing}, StartLocation {crewBooking.StartLocation}");

            var messageId = Guid.NewGuid();
                
            if (await _messageBus.SendAsync(new PilotBookingNotification(messageId, crewBooking)))
            {
                _logger.LogInformation($"BookPilot: Pilot Notified: messageId {messageId}, CrewId {crewBooking.CrewId}, Departing {crewBooking.Departing}, StartLocation {crewBooking.StartLocation}");
                return true;
            }
            else
            {
                _logger.LogError($"BookPilot: Pilot HAS NOT BEEN Notified: messageId {messageId}, CrewId {crewBooking.CrewId}, Departing {crewBooking.Departing}, StartLocation {crewBooking.StartLocation}");
                return false;
            }
        }

        public async Task<CrewBooking> GetBooking(int bookingId)
        {
            return null;
        }
    }
}
