using System;
using System.Threading.Tasks;
using CrewOnDemand.Api.Data;

namespace CrewOnDemand.Api.Services
{
    public interface ICrewMemberService
    {
        Task<CrewMember> PilotAvailableFor(DateTime departing, string fromBase);
    }
}