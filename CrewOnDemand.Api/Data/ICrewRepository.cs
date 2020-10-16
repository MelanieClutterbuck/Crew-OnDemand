using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrewOnDemand.Api.Data
{
    public interface ICrewRepository
    {
        Task<IReadOnlyList<CrewMember>> Get();
    }
}