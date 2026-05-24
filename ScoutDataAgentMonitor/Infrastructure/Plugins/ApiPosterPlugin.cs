using ScoutDataAgentMonitor.Domain.Interfaces;
using ScoutDataAgentMonitor.Domain.Models;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ScoutDataAgentMonitor.Configuration;

namespace ScoutDataAgentMonitor.Infrastructure.Plugins;
public class ApiPosterPlugin : IMonitorPlugin
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _settings;

    public ApiPosterPlugin(
        HttpClient httpClient,
        IOptions<ApiSettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    public async Task ExecuteAsync(SystemMetrics metrics)
    {
        try
        {
            var payload = new
            {
                cpu = metrics.CpuUsage,
                ram_used = metrics.RamUsedMB,
                disk_used = metrics.DiskUsedMB
            };

            string json =
                JsonSerializer.Serialize(payload);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            await _httpClient.PostAsync(
                _settings.Endpoint,
                content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"API Error : {ex.Message}");
        }
    }
}
