using System;
using System.Collections.Generic;
using System.Text;

namespace hydra.comunicaciones.models.estado
{
    public class TaskQeueStatusModel
    {
        public int LocalQueueLength { get; set; } = 0;  
        public int PriorityQueueLength { get; set; } = 0;
        public int GeneralQueueLength { get; set; } = 0;    

        public int LocalTasksInProgress { get; set; } = 0;  
        public int PriorityTasksInProgress { get; set; } = 0;   
        public int GeneralTasksInProgress { get; set; } = 0;    

        public int LocalTasksQueued { get; set; } = 0;
        public int PriorityTasksQueued { get; set; } = 0;   
        public int GeneralTasksQueued { get; set; } = 0;    

        public int TaskCapacity
        {
            get
            {
                return LocalQueueLength + PriorityQueueLength + GeneralQueueLength;
            }
        }

        public int TasksInProgress
        {
            get
            {
                return LocalTasksInProgress + PriorityTasksInProgress + GeneralTasksInProgress;
            }
        }  
        
        public int TasksQueued
        {
            get
            {
                return LocalTasksQueued + PriorityTasksQueued + GeneralTasksQueued;
            }
        }

        public double Rank {
            get
            {
                int local= (LocalQueueLength - LocalTasksInProgress - LocalTasksQueued)*1000;
                int priority = (PriorityQueueLength - PriorityTasksInProgress - PriorityTasksQueued)*100;
                int general = (GeneralQueueLength - GeneralTasksInProgress - GeneralTasksQueued);
                if(this.LocalQueueLength == 0 && this.PriorityQueueLength == 0 && this.GeneralQueueLength == 0)
                {
                    return 0; // Devolvemos 0 indicando que no puede recibir tareas
                }  else { 
                    return (local + priority + general) / (this.LocalQueueLength * 1000 + this.PriorityQueueLength * 100 + this.GeneralQueueLength);
                }
            }
        }   
    }
}
