using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraLfvbDaemon.Hubs.HydraWebSocket
{
    public delegate void LocalTaskPushHandler(string command, object payload);


    public class HydraWebSocketClient : IHydraWebSocketClient
    {

        private HubConnection? _conSERVER;
        private IConfiguration _conf;
        private ILogger<HydraWebSocketClient> _logger;
        public LocalTaskPushHandler onLocalTaskPush;

        public HydraWebSocketClient(IConfiguration conf, ILogger<HydraWebSocketClient> logger)
        {
            this._conf = conf;
            this._logger = logger;
        }

        public void Connect()
        {
            try
            {
                _logger.LogInformation("Intentando conectar al servidor de comandos...");
                this._conSERVER = new HubConnectionBuilder()
                    .WithUrl(this._conf.GetValue<string>("Conexiones:ws:client:url"))
                    .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(this._conf.GetValue<int>("Conexiones:ws:client:reintento")) })
                    .Build();

                _logger.LogInformation("Estableciendo los eventos y los manejadores");

                //Definimos las respuestas
                _conSERVER.On<string, object>("onLocalTaskPush", (command, payload) =>
                {
                    // Aquí puedes manejar los comandos recibidos desde el servidor
                    Console.WriteLine($"Comando recibido: {command}, Payload: {payload}");
                    if (onLocalTaskPush != null)
                    {
                        onLocalTaskPush.Invoke(command, payload);
                    }
                });


                _logger.LogInformation("Iniciando comunicación con el servidor de API (WebSockets)");
                _conSERVER.StartAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al conectar al servidor de comandos.");
            }
        }

        public Task SendCommand(string command, object payload)
        {
            if (_conSERVER == null || _conSERVER.State != HubConnectionState.Connected)
            {
                throw new InvalidOperationException("Not connected to the server.");
            }
            return _conSERVER.InvokeAsync("ReceiveCommand", command, payload);
        }

        public Task CloseConnection(CancellationToken cancellationToken = default)
        {
            if (_conSERVER == null || _conSERVER.State != HubConnectionState.Connected)
            {
                throw new InvalidOperationException("Not connected to the server.");
            }
            return _conSERVER.StopAsync(cancellationToken);
        }
    }
}
