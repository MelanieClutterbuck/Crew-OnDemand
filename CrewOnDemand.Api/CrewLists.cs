using System.Collections.Generic;
using CrewOnDemand.Api.Data;

namespace CrewOnDemand.Api
{
    public class CrewLists {
    public List<int> IdsOfPotentialCrew { get; }
    public List<int> IdsOfPotentialCrewWithBookings { get; }
    public List<CrewBooking> PotentialCrewMembersBookings { get; }

        public CrewLists(List<int> idsOfPotentialCrew, List<int> idsOfPotentialCrewWithBookings, List<CrewBooking> potentialCrewMembersBookings)
        {
            this.IdsOfPotentialCrew = idsOfPotentialCrew;
            this.IdsOfPotentialCrewWithBookings = idsOfPotentialCrewWithBookings;
            this.PotentialCrewMembersBookings = potentialCrewMembersBookings;
        }
    }
}