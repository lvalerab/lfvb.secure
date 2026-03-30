using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraLfvbDaemon.Hubs
{
    public class HydraHub : IHostedService
    {
        private readonly ILogger<HydraHub> _logger;
        private HubConnection _connection;

        public HydraHub(ILogger<HydraHub> logger)
        {
            this._logger = logger;  

            this._connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/hydrahub")
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    await _connection.StartAsync(cancellationToken);
                    _logger.LogInformation("Connected to HydraHub.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to connect to HydraHub. Retrying in 5 seconds...");
                    Task.Delay(5000, cancellationToken).Wait(cancellationToken);
                }
            }   
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _connection.StopAsync(cancellationToken);
        }
    }
}
