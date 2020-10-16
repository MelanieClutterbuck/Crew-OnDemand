using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using CrewOnDemand.Api.Data;

namespace CrewOnDemand.Api.Models
{
    public class BookingInputModel
    {
        [Required] public int Id { get; set; }
        [Required] public short Duration { get; set; }
        [Required] public string StartLocation { get; set; }
        [Required] public string EndLocation { get; set; }
        [Required] public DateTime Departing { get; set; }
        [Required] public DateTime Returning { get; set; }

        public CrewBooking ToBooking()
        {
            var booking = new CrewBooking()
            {
                Id = this.Id,
                Departing = this.Departing,
                Duration = this.Duration,
                EndLocation = this.EndLocation,
                Returning = this.Returning,
                StartLocation = this.StartLocation
            };

            return booking;
        }
    }
}