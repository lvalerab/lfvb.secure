using HydraLfvbDaemon;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<HydraWorker>();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var host = builder.Build();
host.Run();
