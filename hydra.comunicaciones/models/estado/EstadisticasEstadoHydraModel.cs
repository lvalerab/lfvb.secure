using System;
using System.Collections.Generic;
using System.Text;

namespace hydra.comunicaciones.models.estado
{
    public class EstadisticasEstadoHydraModel
    {
        public CpuModel Cpu { get; set; } = new CpuModel();
        public double MemoryUsage { get; set; } = 0;  
        public TaskQeueStatusModel Task { get; set; } = new TaskQeueStatusModel();  
        public double Rank { get
            {
                // Calculamos el rank combinando el uso de CPU, memoria y la capacidad de tareas
                double cpuScore = Cpu.UsagePercentage; // El uso de CPU ya está en porcentaje
                double memoryScore = MemoryUsage; // El uso de memoria también se asume que está en porcentaje
                double taskScore = Task.Rank * 100; // Convertimos el rank de tareas a porcentaje
                // Damos un peso a cada componente (puedes ajustar estos pesos según tus necesidades)
                double totalScore = (cpuScore * 0.5) + (memoryScore * 0.3) + (taskScore * 0.2);
                return totalScore;
            }
        }   
    }
}
