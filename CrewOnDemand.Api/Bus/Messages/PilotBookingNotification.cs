using System;
using CrewOnDemand.Api.Data;

namespace CrewOnDemand.Api.Bus.Messages
{
    public class PilotBookingNotification : IMessage
    {
        public PilotBookingNotification(Guid id, CrewBooking booking)
        {
            this.Id = id;
            this.Message = $"{booking.CrewId} {booking.StartLocation} {booking.EndLocation} {booking.Departing} {booking.Returning}";
        }
        public Guid Id { get; }
        public string Message { get; }
    }
}