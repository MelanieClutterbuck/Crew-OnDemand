using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrewOnDemand.Api.Bus
{
    public class MessageBus : IMessageBus
    {
        public async Task<bool> SendAsync(IMessage message)
        {
            return true;
        }
    }
}
