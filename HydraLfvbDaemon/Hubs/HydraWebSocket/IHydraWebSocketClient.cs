using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraLfvbDaemon.Hubs.HydraWebSocket
{
    public interface IHydraWebSocketClient
    {
        public void Connect();
        public Task CloseConnection(CancellationToken cancellationToken = default);
        public Task SendCommand(string command, object payload);
    }
}
