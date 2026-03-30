using HydraLfvbDaemon.Hubs;
using HydraLfvbDaemon.Hubs.Hydra;
using HydraLfvbDaemon.Hubs.LocalWeb;
using HydraLfvbDaemon.NotifyIcon;
using Microsoft.AspNetCore.SignalR;

namespace HydraLfvbDaemon
{
    public class HydraWorker : BackgroundService
    {
        private readonly ILogger<HydraWorker> _logger;        
        private readonly INotififyIcon _notifyIcon;
        //private readonly IHubContext<HydraCommands,IHydraCommands> _hydraHubContext;
        //private readonly IHubContext<LocalWebCommands,ILocalWebCommands> _hydraLocalWebHubContext;

        public HydraWorker(ILogger<HydraWorker> logger
                           //IHubContext<HydraHub, IHydraCommands> hydraHubContext,
                           //IHubContext<LocalWebHydraHub, ILocalWebCommands> hydraLocalWebHubContext
                      ) : base(
                    )
        {
            _logger = logger;
            //_hydraHubContext = hydraHubContext;
            //_hydraLocalWebHubContext = hydraLocalWebHubContext;
            _notifyIcon = NotifyIconFactory.Create();
        }

        /// <summary>
        /// Bucle principal del servicio en segundo plano. Se ejecuta continuamente hasta que se solicita la cancelación.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                //_ = await _notifyIcon.Init();

                while (!stoppingToken.IsCancellationRequested)
                {
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the HydraWorker.");

                Environment.Exit(1);
            }
        }
    }
}
