using System.Threading.Tasks;
using CrewOnDemand.Api.Data;

namespace CrewOnDemand.Api.Services
{
    public interface ICrewBookingService
    {
        Task<bool> BookPilot(CrewMember crewMember, CrewBooking crewBooking);
        Task<CrewBooking> GetBooking(int bookingId);
    }
}