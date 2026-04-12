using hydra.comunicaciones.Http;
using HydraLfvbDaemon;
using HydraLfvbDaemon.Hubs.Hydra;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<HydraWorker>();
builder.Services.AddSingleton<IHydraCommands,HydraCommands>();
builder.Services.AddSingleton<IHttpRestClient, HttpRestClient>();   

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var host = builder.Build();

host.Run();
