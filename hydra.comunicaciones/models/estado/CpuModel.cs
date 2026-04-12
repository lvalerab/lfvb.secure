using System;
using System.Collections.Generic;
using System.Text;

namespace hydra.comunicaciones.models.estado
{
    public class CpuModel
    {
        public double UserTime { get; set; } = 0;
        public double SystemTime { get; set; } = 0;
        public double TotalTime { get; set; } = 0;
        public double UsagePercentage { get
            {
                return TotalTime > 0 ? (UserTime + SystemTime) / TotalTime * 100 : 0;
            }  
        }
    }
}
