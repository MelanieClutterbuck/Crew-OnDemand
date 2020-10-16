using System;
using System.Reflection.Metadata.Ecma335;

namespace CrewOnDemand.Api.Data
{
    public class CrewBooking
    {
        public int Id { get; set; }
        public int CrewId { get; set; }
        public short Duration { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime Departing { get; set; }
        public DateTime Returning { get; set; }
    }
}