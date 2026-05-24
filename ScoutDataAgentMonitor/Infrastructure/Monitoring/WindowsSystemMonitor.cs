using ScoutDataAgentMonitor.Domain.Interfaces;
using ScoutDataAgentMonitor.Domain.Models;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Management;
using System.Runtime.Versioning;


namespace ScoutDataAgentMonitor.Infrastructure.Monitoring;

//[SupportedOSPlatform("windows")]
public class WindowsSystemMonitor : ISystemMonitor
{
    private readonly PerformanceCounter _cpuCounter;

    public WindowsSystemMonitor()
    {
        _cpuCounter = new PerformanceCounter(
            "Processor",
            "% Processor Time",
            "_Total");

        _cpuCounter.NextValue();

        Thread.Sleep(1000);
    }

    public Task<SystemMetrics> GetMetricsAsync()
    {
        double cpu =
            Math.Round(_cpuCounter.NextValue(), 2);

        // RAM INFO
        //This will calculate the project RAM details
        //var memoryInfo = GC.GetGCMemoryInfo();

        //double totalRam =
        //    memoryInfo.TotalAvailableMemoryBytes
        //    / 1024.0 / 1024.0;

        //double usedRam =
        //    GC.GetTotalMemory(false)
        //    / 1024.0 / 1024.0;

        // This will calculate the whole system RAM details
        var (usedRam, totalRam) = GetMemoryInfo();

        // DISK INFO
        DriveInfo drive = DriveInfo.GetDrives()
            .First(x => x.IsReady);

        double totalDisk =
            drive.TotalSize / 1024.0 / 1024.0;

        double freeDisk =
            drive.AvailableFreeSpace / 1024.0 / 1024.0;

        double usedDisk = totalDisk - freeDisk;

        return Task.FromResult(new SystemMetrics
        {
            CpuUsage = cpu,

            RamUsedMB = Math.Round(usedRam, 2),

            RamTotalMB = Math.Round(totalRam, 2),

            DiskUsedMB = Math.Round(usedDisk, 2),

            DiskTotalMB = Math.Round(totalDisk, 2),

            Timestamp = DateTime.Now
        });
    }

    private (double UsedRamMb, double TotalRamMb) GetMemoryInfo()
    {
        double totalRamMb = 0;
        double freeRamMb = 0;

        using var searcher =
            new ManagementObjectSearcher(
                "SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");

        foreach (ManagementObject obj in searcher.Get())
        {
            totalRamMb =
                Convert.ToDouble(obj["TotalVisibleMemorySize"]) / 1024;

            freeRamMb =
                Convert.ToDouble(obj["FreePhysicalMemory"]) / 1024;
        }

        return (
            totalRamMb - freeRamMb,
            totalRamMb
        );
    }
}
