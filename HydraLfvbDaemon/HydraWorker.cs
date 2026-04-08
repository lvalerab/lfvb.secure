using HydraLfvbDaemon.Hubs;
using HydraLfvbDaemon.Hubs.Hydra;
using HydraLfvbDaemon.Hubs.LocalWeb;
using Microsoft.AspNetCore.SignalR;

namespace HydraLfvbDaemon
{
    public class HydraWorker : BackgroundService
    {
        private readonly ILogger<HydraWorker> _logger;
        private IConfiguration _config;

        private IHydraCommands _hydraHubContext;    

        private PriorityQueue<string, int> tareasLocales = new PriorityQueue<string, int>();
        private PriorityQueue<string, int> tareasPrioritarias = new PriorityQueue<string, int>();
        private PriorityQueue<string, int> tareasGenerales = new PriorityQueue<string, int>();

        private PriorityQueue<Task, int> colaEjecucion = new PriorityQueue<Task, int>();

        //private readonly IHubContext<HydraCommands,IHydraCommands> _hydraHubContext;
        //private readonly IHubContext<LocalWebCommands,ILocalWebCommands> _hydraLocalWebHubContext;

        public HydraWorker(ILogger<HydraWorker> logger,
                           IConfiguration config,
                           IHydraCommands hydraCommands
                      ) : base(
                    )
        {
            _logger = logger;
            _config = config;            
            _hydraHubContext = hydraCommands;

            this._hydraHubContext.Connect();    
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
                while (!stoppingToken.IsCancellationRequested)
                {
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }
                    //Mandamos la situacion del equipo y de las colas a traves de signalR al gestor de colas y al web local
                    //La solicitud de tareas, y demas, se encarga signalR de enviarla a traves de los hubs correspondientes,
                    //y se encarga de añadir las tareas a las colas correspondientes, segun su prioridad y su origen,
                    //y el worker se encarga de ejecutar las tareas en funcion de su prioridad
                    //y de su orden de llegada a la cola, y de comprobar que las tareas lanzadas
                    //hayan terminado para lanzar las siguientes tareas en funcion de su prioridad y de su orden de llegada a la cola
                    EnviarEstadoHydra();

                    //Comprobamos que las Tareas lanzadas, hayan terminado, si es asi, comprobamos primero las tareas locales, despues las prioritarias y por ultimo las normales, para lanzar las siguientes tareas en funcion de su prioridad y de su orden de llegada a la cola
                    ComprobacionDeColas();

                    await Task.Delay(_config.GetValue<int>("main:bucle:wait"), stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("HydraWorker is stopping due to cancellation request.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the HydraWorker.");

                Environment.Exit(1);
            }
        }

        public async Task EnviarEstadoHydra()
        {
            try
            {
                //Aquí iría la lógica para enviar el estado del equipo y de las colas a través de SignalR al gestor de colas y al web local.
                //Podrías usar los contextos de los hubs para enviar mensajes a los clientes conectados con la información relevante.
                _logger.LogInformation("Sending Hydra state to clients.");
                _logger.LogInformation("CPU Usage: {userTime}% {PrivilegTime}% {TotalTime}",Environment.CpuUsage.UserTime.Ticks/Environment.CpuUsage.TotalTime.Ticks, Environment.CpuUsage.PrivilegedTime.Ticks/Environment.CpuUsage.TotalTime.Ticks, Environment.CpuUsage.TotalTime);
                _logger.LogInformation("Memory Usage: {memoryUsage} MB",Environment.WorkingSet / (1024 * 1024));
                _logger.LogInformation("Local Tasks Queue Length: {localQueueLength}", tareasLocales.Count);
                _logger.LogInformation("Priority Tasks Queue Length: {priorityQueueLength}", tareasPrioritarias.Count); 
                _logger.LogInformation("General Tasks Queue Length: {generalQueueLength}", tareasGenerales.Count);  
                _logger.LogInformation("Execution Queue Length: {executionQueueLength}", colaEjecucion.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending the state of Hydra.");
            }
        }


        public void ComprobacionDeColas()
        {
            try
            {
                //Comprobamos que las Tareas lanzadas, hayan terminado, si es asi, comprobamos primero las tareas locales, despues las prioritarias y por ultimo las normales, para lanzar las siguientes tareas en funcion de su prioridad y de su orden de llegada a la cola
                for (var i = 0; i < colaEjecucion.Count; i++)
                {
                    var tarea = colaEjecucion.Dequeue();
                    if (tarea.IsCompleted)
                    {
                        _logger.LogInformation("Tarea completa {taskId}", tarea.Id);
                        //Obtenemos una tarea, primero de la cola de tareas locales, si no hay, de la cola de tareas prioritarias, y si no hay, de la cola de tareas generales
                        if (this.tareasLocales.Count > 0)
                        {
                            var tareaLocal = tareasLocales.Dequeue();
                            colaEjecucion.Enqueue(Task.Run(() => EjecutarTarea(tareaLocal)), i); // Puedes ajustar la prioridad según sea necesario.
                        }
                        else if (this.tareasPrioritarias.Count > 0)
                        {
                            var tareaPrioritaria = tareasPrioritarias.Dequeue();
                            colaEjecucion.Enqueue(Task.Run(() => EjecutarTarea(tareaPrioritaria)), i); // Puedes ajustar la prioridad según sea necesario.
                        }
                        else if (this.tareasGenerales.Count > 0)
                        {
                            var tareaGeneral = tareasGenerales.Dequeue();
                            colaEjecucion.Enqueue(Task.Run(() => EjecutarTarea(tareaGeneral)), i); // Puedes ajustar la prioridad según sea necesario.
                        }
                    }
                    else
                    {
                        colaEjecucion.Enqueue(tarea, i); //volvemos a encolar la tarea para comprobarla en la siguiente iteración del bucle
                    }                
                }
                if(colaEjecucion.Count < _config.GetValue<int>("main:threads:max"))
                {
                    _logger.LogInformation("Completando la cola de ejecucion, desde {inicio} hasta {fin}",colaEjecucion.Count, _config.GetValue<int>("main:threads:max"));
                    int j=colaEjecucion.Count;
                    for (int i = j; i < _config.GetValue<int>("main:threads:max"); i++)
                    {
                        _logger.LogInformation("Encolando tarea en la posicion {tarea}", i + 1);
                        //Si no hay tareas en ejecución, comprobamos si hay tareas en las colas de tareas locales, prioritarias o generales para ejecutar
                        if (this.tareasLocales.Count > 0)
                        {
                            var tareaLocal = tareasLocales.Dequeue();
                            colaEjecucion.Enqueue(Task.Run(() => EjecutarTarea(tareaLocal)), 0); // Puedes ajustar la prioridad según sea necesario.
                        }
                        else if (this.tareasPrioritarias.Count > 0)
                        {
                            var tareaPrioritaria = tareasPrioritarias.Dequeue();
                            colaEjecucion.Enqueue(Task.Run(() => EjecutarTarea(tareaPrioritaria)), 0); // Puedes ajustar la prioridad según sea necesario.
                        }
                        else if (this.tareasGenerales.Count > 0)
                        {
                            var tareaGeneral = tareasGenerales.Dequeue();
                            colaEjecucion.Enqueue(Task.Run(() => EjecutarTarea(tareaGeneral)), 0); // Puedes ajustar la prioridad según sea necesario.
                        } else
                        {
                            _logger.LogWarning("Colas de tareas vacias");                           
                        }
                    }
                }   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Al comprobar las tareas y encolarlas");
            }
        }

        public void EjecutarTarea(string tarea)
        {
            try
            {
                _logger.LogInformation("Ejecuntado tarea ID: {task}", tarea);
                // Aquí iría la lógica para ejecutar la tarea específica.
                // Por ejemplo, podrías usar un switch o if-else para determinar qué acción tomar según el tipo de tarea.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar la tarea: {task}", tarea);
            } finally
            {
                //Limpiar recursos, cerrar conexiones, etc. después de ejecutar la tarea.
                _logger.LogInformation("Finalizada la ejecución de la tarea: {task}", tarea);
                //Llamamos al recolector de memoria
                GC.Collect();
            }
        }
    }
}
