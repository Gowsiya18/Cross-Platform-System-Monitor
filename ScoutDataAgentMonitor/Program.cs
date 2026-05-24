using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScoutDataAgentMonitor.Configuration;
using ScoutDataAgentMonitor.Domain.Interfaces;
using ScoutDataAgentMonitor.Infrastructure.Monitoring;
using ScoutDataAgentMonitor.Infrastructure.Plugins;
using ScoutDataAgentMonitor.Infrastructure.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<MonitoringSettings>(
            context.Configuration.GetSection("Monitoring"));

        services.Configure<ApiSettings>(
            context.Configuration.GetSection("ApiSettings"));

        services.AddSingleton<ISystemMonitor,
            WindowsSystemMonitor>();

        services.AddSingleton<IMonitorPlugin,
            FileLoggerPlugin>();

        services.AddHttpClient<ApiPosterPlugin>();

        services.AddSingleton<IMonitorPlugin>(sp =>
            sp.GetRequiredService<ApiPosterPlugin>());

        services.AddSingleton<MonitoringService>();
    })
    .Build();

var service =
    host.Services.GetRequiredService<MonitoringService>();

await service.StartAsync();
