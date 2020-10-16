using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrewOnDemand.Api.Data
{
    public interface ICrewBookingRepository
    {
        Task<IReadOnlyList<CrewBooking>> Get(int crewMemberId);
        Task<IReadOnlyList<CrewBooking>> Get();
        Task<bool> Add(CrewBooking crewBooking);
    }
}
