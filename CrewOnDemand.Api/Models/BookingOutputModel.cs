using System;
using CrewOnDemand.Api.Data;

namespace CrewOnDemand.Api.Models
{
    public class BookingOutputModel
    {
        public int Id { get; set; }
        public short Duration { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime Departing { get; set; }
        public DateTime Returning { get; set; }
        public string PilotName { get; set; }

        public BookingOutputModel() { }

        private BookingOutputModel(CrewBooking booking)
        {
            this.PilotName = "Betty";
            this.StartLocation = booking.StartLocation;
            this.EndLocation = booking.EndLocation;
            this.Departing = booking.Departing;
            this.Returning = booking.Returning;
            this.Duration = 1;
        }

        public static BookingOutputModel FromBooking(CrewBooking booking)
        {
            _ = booking ?? throw new ArgumentNullException(nameof(booking));

            return new BookingOutputModel(booking);
        }

    }
}