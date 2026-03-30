using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HydraLfvbDaemon.NotifyIcon
{
    public class NotifyIconFactory
    {
        public static INotififyIcon Create()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WINDOWS.NotifyIconWindows();
            }
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new LINUX.NotifyIconLinux();
            }
            return new DEFAULT.NotifyIconDefault() ;
        }
    }
}
