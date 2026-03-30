using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HydraLfvbDaemon.NotifyIcon.WINDOWS
{
    internal class NotifyIconWindows : INotififyIcon
    {
        public Task<bool> Init()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetConnectionStatus(string status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetIcon(string ruta)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetInfoColas(string cola, string maxItems, string currentItems)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetInfoSystem()
        {
            throw new NotImplementedException();
        }
    }
}
