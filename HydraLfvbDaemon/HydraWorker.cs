using hydra.comunicaciones.Http;
using hydra.comunicaciones.models.estado;
using hydra.comunicaciones.models.tareas;
using HydraLfvbDaemon.Hubs.Hydra;


namespace HydraLfvbDaemon
{
    public class HydraWorker : BackgroundService
    {
        private readonly ILogger<HydraWorker> _logger;
        private IConfiguration _config;

        private IHydraCommands _hydraHubContext;
        private IHttpRestClient _clienteRest;

        private PriorityQueue<TareaModel, int> tareasLocales = new PriorityQueue<TareaModel, int>();
        private PriorityQueue<TareaModel, int> tareasPrioritarias = new PriorityQueue<TareaModel, int>();
        private PriorityQueue<TareaModel, int> tareasGenerales = new PriorityQueue<TareaModel, int>();

        private PriorityQueue<TareaModel, int> colaEjecucion = new PriorityQueue<TareaModel, int>();

        public HydraWorker(ILogger<HydraWorker> logger,
                           IConfiguration config,
                           IHydraCommands hydraCommands,
                           IHttpRestClient clienteRest
                      ) : base(
                    )
        {
            _logger = logger;
            _config = config;            
            _hydraHubContext = hydraCommands;
            _clienteRest = clienteRest;

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
                DateTime finicio = DateTime.Now;
                DateTime flimpieza=DateTime.Now;
                while (!stoppingToken.IsCancellationRequested)
                {
                    TimeSpan tiempoInicioBucle = DateTime.Now - finicio;
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }

                    try
                    {
                        var aux = await _clienteRest.Get<string>(_config.GetValue<string>("Conexiones:sw:url") + "/api/Utils/ping",false);
                        _logger.LogInformation("valor de la llamada {result}", aux);
                    }
                    catch (Exception err)
                    {
                        _logger.LogError("No se ha conseguido realizar la llamada rest");
                    }

                    //Todas estas tareas dan igual el orden de ejecución, pero esperamos a que terminen todas para continuar con otro bucle
                    Task.WaitAll([
                            //Mandamos la situacion del equipo y de las colas a traves de signalR al gestor de colas y al web local
                            //La solicitud de tareas, y demas, se encarga signalR de enviarla a traves de los hubs correspondientes,
                            //y se encarga de añadir las tareas a las colas correspondientes, segun su prioridad y su origen,
                            //y el worker se encarga de ejecutar las tareas en funcion de su prioridad
                            //y de su orden de llegada a la cola, y de comprobar que las tareas lanzadas
                            //hayan terminado para lanzar las siguientes tareas en funcion de su prioridad y de su orden de llegada a la cola
                            EnviarEstadoHydra(),
                            //Comprobamos que las Tareas lanzadas, hayan terminado, si es asi, comprobamos primero las tareas locales, despues las prioritarias y por ultimo las normales, para lanzar las siguientes tareas en funcion de su prioridad y de su orden de llegada a la cola
                            ComprobacionDeColas()
                        ]
                    );

                    TimeSpan tiempoFinBucle = DateTime.Now - finicio;

                    _logger.LogInformation("Tiempo total de ejecución del bucle: {tbucle}", tiempoFinBucle - tiempoInicioBucle);
                    _logger.LogInformation("Tiempo total de ejecución: {ttotal} minutos", tiempoFinBucle.TotalMinutes);

                    //Por cada 30 minutos, forzamos a la liberacion de recursos
                    if((DateTime.Now-flimpieza).TotalMinutes>=_config.GetValue<int>("main:bucle:gc:tiempo"))
                    {
                        flimpieza = DateTime.Now;   
                        LiberarRecursos();
                    }

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

        public async Task LiberarRecursos()
        {
            try { 
                _logger.LogInformation("Liberacion de recursos obligatorio");
                GC.Collect();
            } catch (Exception err)
            {
                _logger.LogError("Error al liberar recursos", err);
            }
        }

        public async Task EnviarEstadoHydra()
        {
            try
            {
                EstadisticasEstadoHydraModel datos = new EstadisticasEstadoHydraModel
                {
                    Cpu = new CpuModel
                    {
                        UserTime = Environment.CpuUsage.UserTime.TotalNanoseconds,
                        SystemTime = Environment.CpuUsage.PrivilegedTime.TotalNanoseconds,
                        TotalTime = Environment.CpuUsage.TotalTime.TotalNanoseconds
                    },
                    MemoryUsage = Environment.WorkingSet / (1024 * 1024), // Convertir a MB
                    Task = new TaskQeueStatusModel
                    {
                        LocalQueueLength = tareasLocales.Count,
                        PriorityQueueLength = tareasPrioritarias.Count,
                        GeneralQueueLength = tareasGenerales.Count,
                    }
                };
                //Aquí iría la lógica para enviar el estado del equipo y de las colas a través de SignalR al gestor de colas y al web local.
                //Podrías usar los contextos de los hubs para enviar mensajes a los clientes conectados con la información relevante.
                _logger.LogInformation("Sending Hydra state to clients.");
                _logger.LogInformation("Datos: {datos}",datos);
                _logger.LogInformation("Rango calculado: {rango}",datos.Rank);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending the state of Hydra.");
            }
        }


        public async Task ComprobacionDeColas()
        {
            try
            {
                //Comprobamos que las Tareas lanzadas, hayan terminado, si es asi, comprobamos primero las tareas locales, despues las prioritarias y por ultimo las normales, para lanzar las siguientes tareas en funcion de su prioridad y de su orden de llegada a la cola
                for (var i = 0; i < colaEjecucion.Count; i++)
                {
                    var tarea = colaEjecucion.Dequeue();
                    if (tarea.tarea != null && tarea.tarea.IsCompleted)
                    {
                        _logger.LogInformation("Tarea completa {taskId}", tarea.id);
                        //Obtenemos una tarea, primero de la cola de tareas locales, si no hay, de la cola de tareas prioritarias, y si no hay, de la cola de tareas generales
                        if (this.tareasLocales.Count > 0)
                        {
                            var tareaLocal = tareasLocales.Dequeue();
                            tareaLocal.tarea = Task.Run(() => EjecutarTarea(tareaLocal)); // Asignamos la tarea al modelo para poder comprobar su estado en la siguiente iteración del bucle
                            colaEjecucion.Enqueue(tareaLocal, i); // Puedes ajustar la prioridad según sea necesario.
                        }
                        else if (this.tareasPrioritarias.Count > 0)
                        {
                            var tareaPrioritaria = tareasPrioritarias.Dequeue();
                            tareaPrioritaria.tarea = Task.Run(() => EjecutarTarea(tareaPrioritaria)); // Asignamos la tarea al modelo para poder comprobar su estado en la siguiente iteración del bucle
                            colaEjecucion.Enqueue(tareaPrioritaria, i); // Puedes ajustar la prioridad según sea necesario.
                        }
                        else if (this.tareasGenerales.Count > 0)
                        {
                            var tareaGeneral = tareasGenerales.Dequeue();
                            tareaGeneral.tarea = Task.Run(() => EjecutarTarea(tareaGeneral)); // Asignamos la tarea al modelo para poder comprobar su estado en la siguiente iteración del bucle
                            colaEjecucion.Enqueue(tareaGeneral, i); // Puedes ajustar la prioridad según sea necesario.
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
                            tareaLocal.tarea = Task.Run(() => EjecutarTarea(tareaLocal)); // Asignamos la tarea al modelo para poder comprobar su estado en la siguiente iteración del bucle    
                            colaEjecucion.Enqueue(tareaLocal, 0); // Puedes ajustar la prioridad según sea necesario.
                        }
                        else if (this.tareasPrioritarias.Count > 0)
                        {
                            var tareaPrioritaria = tareasPrioritarias.Dequeue();
                            tareaPrioritaria.tarea = Task.Run(() => EjecutarTarea(tareaPrioritaria)); // Asignamos la tarea al modelo para poder comprobar su estado en la siguiente iteración del bucle
                            colaEjecucion.Enqueue(tareaPrioritaria, 0); // Puedes ajustar la prioridad según sea necesario.
                        }
                        else if (this.tareasGenerales.Count > 0)
                        {
                            var tareaGeneral = tareasGenerales.Dequeue();
                            tareaGeneral.tarea = Task.Run(() => EjecutarTarea(tareaGeneral)); // Asignamos la tarea al modelo para poder comprobar su estado en la siguiente iteración del bucle
                            colaEjecucion.Enqueue(tareaGeneral, 0); // Puedes ajustar la prioridad según sea necesario.
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

        public void EjecutarTarea(TareaModel tarea)
        {
            try
            {
                _logger.LogInformation("Ejecuntado tarea ID: {task} con prioridad: {priority}   ", tarea.id, tarea.prioridad);
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
