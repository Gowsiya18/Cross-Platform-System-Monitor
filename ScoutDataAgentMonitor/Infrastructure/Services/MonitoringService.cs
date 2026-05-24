using Microsoft.Extensions.Options;
using ScoutDataAgentMonitor.Configuration;
using ScoutDataAgentMonitor.Domain.Interfaces;

namespace ScoutDataAgentMonitor.Infrastructure.Services;
public class MonitoringService
{
    private readonly ISystemMonitor _monitor;
    private readonly IEnumerable<IMonitorPlugin> _plugins;
    private readonly MonitoringSettings _settings;

    public MonitoringService(
        ISystemMonitor monitor,
        IEnumerable<IMonitorPlugin> plugins,
        IOptions<MonitoringSettings> settings)
    {
        _monitor = monitor;
        _plugins = plugins;
        _settings = settings.Value;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            try
            {
                var metrics =
                    await _monitor.GetMetricsAsync();

                Console.Clear();

                Console.WriteLine(
                    $"Time : {metrics.Timestamp}");

                Console.WriteLine(
                    $"CPU Usage : {metrics.CpuUsage}%");

                Console.WriteLine(
                    $"RAM : {metrics.RamUsedMB} MB / {metrics.RamTotalMB} MB");

                Console.WriteLine(
                    $"Disk : {metrics.DiskUsedMB} MB / {metrics.DiskTotalMB} MB");

                foreach (var plugin in _plugins)
                {
                    await plugin.ExecuteAsync(metrics);
                }

                await Task.Delay(
                    TimeSpan.FromSeconds(
                        _settings.IntervalSeconds));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error : {ex.Message}");
            }
        }
    }
}
