using hydra.comunicaciones.Http;
using HydraLfvbDaemon;
using HydraLfvbDaemon.Hubs.HydraWebSocket;



var builder = Host.CreateApplicationBuilder(args);
//Añadimos el servicio de signal R para el servidor local
//builder.Services.AddSignalRCore();  

builder.Services.AddHostedService<HydraWorker>();
builder.Services.AddSingleton<IHydraWebSocketClient,HydraWebSocketClient>(); //Para conectarnos al servidor de API (WebSockets)
builder.Services.AddSingleton<IHttpRestClient, HttpRestClient>();




builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var host = builder.Build();


//host.MapHub<HydraWebSocketServer>("/hydra/local");   

host.Run();
