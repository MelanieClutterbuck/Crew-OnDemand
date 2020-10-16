using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using CrewOnDemand.Api.Data;

namespace CrewOnDemand.Api.UnitTests.TestData
{

    public static class CrewBookingsStub
    {
        public static IReadOnlyList<CrewBooking> GetCrewBookingsList()
        {
            var list = new List<CrewBooking>
            {
                new CrewBooking()
                {
                    CrewId = 1, Duration = 2, EndLocation = "Berlin", 
                    Departing = DateTime.Parse("2020-05-01T09:00:00Z"), Id = 200, 
                    Returning = DateTime.Parse("2020-05-01T11:00:00Z"), StartLocation = "Munich"
                },
                new CrewBooking()
                {
                    CrewId = 2, Duration = 4, EndLocation = "Berlin", 
                    Departing = DateTime.Parse("2020-05-01T09:00:00Z"), Id = 300,
                    Returning = DateTime.Parse("2020-05-01T13:00:00Z"), StartLocation = "Munich"
                },
                new CrewBooking()
                {
                    CrewId = 2, Duration = 4, EndLocation = "Berlin", 
                    Departing = DateTime.Parse("2020-05-02T09:00:00Z"), Id = 303, 
                    Returning = DateTime.Parse("2020-05-02T13:00:00Z"), StartLocation = "Munich"
                },
                new CrewBooking()
                {
                    CrewId = 5, Duration = 2, EndLocation = "Munich", 
                    Departing = DateTime.Parse("2020-05-02T09:00:00Z"), Id = 100, 
                    Returning = DateTime.Parse("2020-05-02T11:00:00Z"), StartLocation = "Berlin"
                }
            };

            return list;
        }

        public static IReadOnlyList<CrewBooking> GetBusyCrewBookingsList()
        {
            var list = new List<CrewBooking>
            {
                new CrewBooking()
                {
                    CrewId = 1, Duration = 2, EndLocation = "Berlin",
                    Departing = DateTime.Parse("2020-05-02T09:00:00Z"), Id = 200,
                    Returning = DateTime.Parse("2020-05-02T11:00:00Z"), StartLocation = "Munich"
                },
                new CrewBooking()
                {
                    CrewId = 2, Duration = 4, EndLocation = "Berlin",
                    Departing = DateTime.Parse("2020-05-02T09:00:00Z"), Id = 300,
                    Returning = DateTime.Parse("2020-05-02T13:00:00Z"), StartLocation = "Munich"
                },
                new CrewBooking()
                {
                    CrewId = 2, Duration = 4, EndLocation = "Berlin",
                    Departing = DateTime.Parse("2020-05-09T09:00:00Z"), Id = 303,
                    Returning = DateTime.Parse("2020-05-09T13:00:00Z"), StartLocation = "Munich"
                },
                new CrewBooking()
                {
                    CrewId = 3, Duration = 4, EndLocation = "Berlin",
                    Departing = DateTime.Parse("2020-05-09T09:00:00Z"), Id = 303,
                    Returning = DateTime.Parse("2020-05-09T13:00:00Z"), StartLocation = "Munich"
                },
                new CrewBooking()
                {
                    CrewId = 4, Duration = 4, EndLocation = "Berlin",
                    Departing = DateTime.Parse("2020-05-09T09:00:00Z"), Id = 303,
                    Returning = DateTime.Parse("2020-05-09T13:00:00Z"), StartLocation = "Munich"
                },
                new CrewBooking()
                {
                    CrewId = 5, Duration = 2, EndLocation = "Munich",
                    Departing = DateTime.Parse("2020-05-02T09:00:00Z"), Id = 100,
                    Returning = DateTime.Parse("2020-05-02T11:00:00Z"), StartLocation = "Berlin"
                },
                new CrewBooking()
                {
                    CrewId = 6, Duration = 2, EndLocation = "Munich",
                    Departing = DateTime.Parse("2020-05-03T09:00:00Z"), Id = 100,
                    Returning = DateTime.Parse("2020-05-03T11:00:00Z"), StartLocation = "Berlin"
                },
                new CrewBooking()
                {
                    CrewId = 7, Duration = 2, EndLocation = "Munich",
                    Departing = DateTime.Parse("2020-05-04T09:00:00Z"), Id = 100,
                    Returning = DateTime.Parse("2020-05-04T11:00:00Z"), StartLocation = "Berlin"
                },
                new CrewBooking()
                {
                    CrewId = 8, Duration = 2, EndLocation = "Munich",
                    Departing = DateTime.Parse("2020-05-05T09:00:00Z"), Id = 100,
                    Returning = DateTime.Parse("2020-05-05T11:00:00Z"), StartLocation = "Berlin"
                },

            };

            return list;
        }

        public static IReadOnlyList<CrewBooking> GetBusySameDayCrewBookingsList()
        {
            var list = new List<CrewBooking>
            {
                new CrewBooking()
                {
                    CrewId = 1, Duration = 2, EndLocation = "Berlin",
                    Departing = DateTime.Parse("2020-05-02T09:00:00Z"), Id = 200,
                    Returning = DateTime.Parse("2020-05-02T11:00:00Z"), StartLocation = "Munich"
                },
                new CrewBooking()
                {
                    CrewId = 2, Duration = 4, EndLocation = "Berlin",
                    Departing = DateTime.Parse("2020-05-02T09:00:00Z"), Id = 300,
                    Returning = DateTime.Parse("2020-05-02T13:00:00Z"), StartLocation = "Munich"
                },
                new CrewBooking()
                {
                    CrewId = 5, Duration = 2, EndLocation = "Munich",
                    Departing = DateTime.Parse("2020-05-02T09:00:00Z"), Id = 100,
                    Returning = DateTime.Parse("2020-05-02T11:00:00Z"), StartLocation = "Berlin"
                }
            };

            return list;
        }
    }
}
