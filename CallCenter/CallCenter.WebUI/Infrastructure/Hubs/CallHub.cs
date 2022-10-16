using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CallCenter.WebUI.Infrastructure.Hubs
{
    public class CallHub : Hub<ITypedHubClient>
    {
        public async Task SendMessage()
        {
            await Clients.All.SendMessageToClient();
        }

        
    }

    public interface ITypedHubClient
    {
        Task SendMessageToClient();
    }
}
