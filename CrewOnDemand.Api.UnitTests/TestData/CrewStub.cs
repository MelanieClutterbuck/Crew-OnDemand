using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrewOnDemand.Api.Data;

namespace CrewOnDemand.Api.UnitTests.TestData
{
    public static class CrewStub
    {
        public static IReadOnlyList<CrewMember> GetCrewMembersList()
        {
            var list = new List<CrewMember>
            {
                new CrewMember()
                {
                    Id = 1, Base = "Munich", IsAvailable = null, Name = "Andy",
                    WorkDays = new string[] {"Monday", "Tuesday", "Thursday", "Saturday"}
                },
                new CrewMember()
                {
                    Id = 2, Base = "Munich", IsAvailable = null, Name = "Betty",
                    WorkDays = new string[] {"Monday", "Tuesday", "Wednesday", "Friday"}
                },
                new CrewMember()
                {
                    Id = 3, Base = "Munich", IsAvailable = null, Name = "Callum",
                    WorkDays = new string[] {"Wednesday", "Thursday", "Saturday", "Sunday"}
                },
                new CrewMember()
                {
                    Id = 4, Base = "Munich", IsAvailable = null, Name = "Daphne",
                    WorkDays = new string[] {"Friday", "Saturday", "Sunday"}
                },
                new CrewMember()
                {
                    Id = 5, Base = "Berlin", IsAvailable = null, Name = "Elvis",
                    WorkDays = new string[] {"Monday", "Tuesday", "Thursday", "Saturday"}
                },
                new CrewMember()
                {
                    Id = 6, Base = "Berlin", IsAvailable = null, Name = "Freida",
                    WorkDays = new string[] {"Monday", "Tuesday", "Wednesday", "Friday"}
                },
                new CrewMember()
                {
                    Id = 7, Base = "Berlin", IsAvailable = null, Name = "Greg",
                    WorkDays = new string[] {"Wednesday", "Thursday", "Saturday", "Sunday"}
                },
                new CrewMember()
                {
                    Id = 8, Base = "Berlin", IsAvailable = null, Name = "Hermione",
                    WorkDays = new string[] {"Friday", "Saturday", "Sunday"}
                }
            };

            return list;
        }

        public static IReadOnlyList<CrewMember> GetBusySameDayCrewMembersList()
        {
            var list = new List<CrewMember>
            {
                new CrewMember()
                {
                    Id = 1, Base = "Munich", IsAvailable = null, Name = "Andy",
                    WorkDays = new string[] {"Monday", "Tuesday", "Thursday", "Saturday"}
                },
                new CrewMember()
                {
                    Id = 2, Base = "Munich", IsAvailable = null, Name = "Betty",
                    WorkDays = new string[] {"Monday", "Tuesday", "Wednesday", "Friday"}
                },
                new CrewMember()
                {
                    Id = 5, Base = "Berlin", IsAvailable = null, Name = "Elvis",
                    WorkDays = new string[] {"Monday", "Tuesday", "Thursday", "Saturday"}
                }
            };

            return list;
        }
    }
}
