using System.Threading;
using System.Threading.Tasks;
using CallCenter.Services.Abstract;
using CallCenter.WebUI.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace CallCenter.WebUI.Infrastructure
{
    public class CallDistributorHostingService : IHostedService
    {
        private Timer _timer;
        private readonly ICallDistributorService _distributor;
        private readonly IHubContext<CallHub, ITypedHubClient> _hub;

        public CallDistributorHostingService(ICallDistributorService distributor,
            IHubContext<CallHub, ITypedHubClient> hub)
        {
            _distributor = distributor;
            _hub = hub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Distribute, null, 0, 5000);
            return Task.CompletedTask;
        }

        private async void Distribute(object state)
        {
            var hadChanges = await _distributor.Distribute();
            if (hadChanges) await _hub.Clients.All.SendMessageToClient();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}