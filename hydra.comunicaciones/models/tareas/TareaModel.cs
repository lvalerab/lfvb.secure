using System;
using System.Collections.Generic;
using System.Text;

namespace hydra.comunicaciones.models.tareas
{
    public class TareaModel
    {
        public Guid id { get; set; }    
        public Task? tarea { get; set; }
        public int prioridad { get; set; }  //0: local, 1:prioritaria, 2: normal
    }
}
