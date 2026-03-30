using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydraLfvbDaemon.NotifyIcon
{
    public interface INotififyIcon
    {
        public Task<bool> Init();
        public Task<bool> SetIcon(string ruta);
        public Task<bool> SetConnectionStatus(string status);
        public Task<bool> SetInfoColas(string cola, string maxItems, string currentItems);
        public Task<bool> SetInfoSystem();
    }
}
