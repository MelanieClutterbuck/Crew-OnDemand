using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CrewOnDemand.Api.Data
{
    public class JsonCrewBookingRepository : ICrewBookingRepository
    {
        private readonly ILogger<JsonCrewBookingRepository> _logger;
        private readonly IConfiguration _config;

        public JsonCrewBookingRepository(IConfiguration config, ILogger<JsonCrewBookingRepository> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<IReadOnlyList<CrewBooking>> Get(int crewMemberId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Add(CrewBooking crewBooking)
        {
            return true;
        }

        public async Task<IReadOnlyList<CrewBooking>> Get()
        {
            _logger.LogInformation("Started Get.");

            var file = _config.GetValue<string>("jsoncrewbookings");

            var jsonString = await File.ReadAllTextAsync(file);
            var bookings = new List<CrewBooking>();
            
            using var document = JsonDocument.Parse(jsonString);

            var root = document.RootElement;
            var crewBookings = root.GetProperty("CrewBookings");
            foreach (var crewMember in crewBookings.EnumerateArray())
            {
                foreach (var bookingElement in crewMember.GetProperty("Bookings").EnumerateArray())
                {
                    var booking = JsonSerializer.Deserialize<CrewBooking>(bookingElement.ToString());
                    
                    booking.CrewId = crewMember.GetProperty("CrewId").GetInt32();

                    booking.Departing = DateTime.Parse(bookingElement.GetProperty("Departure").GetString());
                           
                    booking.Returning = DateTime.Parse(bookingElement.GetProperty("Return").GetString());

                    bookings.Add(booking);
                }
            }

            _logger.LogInformation("Completed Get.");

            return bookings.AsReadOnly();
        }
    }
}
