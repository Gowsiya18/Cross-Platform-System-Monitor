using Microsoft.Extensions.Options;
using ScoutDataAgentMonitor.Configuration;
using ScoutDataAgentMonitor.Domain.Interfaces;
using ScoutDataAgentMonitor.Domain.Models;

namespace ScoutDataAgentMonitor.Infrastructure.Plugins;

public class FileLoggerPlugin : IMonitorPlugin
{
    private readonly string _logFilePath;

    public FileLoggerPlugin(
        IOptions<ApiSettings> options)
    {
        _logFilePath = Path.Combine(
            AppContext.BaseDirectory,
            options.Value.LogFilePath);

        Directory.CreateDirectory(
            Path.GetDirectoryName(_logFilePath)!);
    }

    public async Task ExecuteAsync(SystemMetrics metrics)
    {
        Console.WriteLine("FileLoggerPlugin Executed");

        string logEntry =
            $"{metrics.Timestamp:yyyy-MM-dd HH:mm:ss} | " +
            $"CPU:{metrics.CpuUsage}% | " +
            $"RAM:{metrics.RamUsedMB}/{metrics.RamTotalMB} MB | " +
            $"DISK:{metrics.DiskUsedMB}/{metrics.DiskTotalMB} MB";

        await File.AppendAllTextAsync(
            _logFilePath,
            logEntry + Environment.NewLine);
    }
}
