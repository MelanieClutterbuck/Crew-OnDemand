
using System.Threading.Tasks;

namespace CrewOnDemand.Api.Bus
{
    public interface IMessageBus
    {
        Task<bool> SendAsync(IMessage message);
    }
}
