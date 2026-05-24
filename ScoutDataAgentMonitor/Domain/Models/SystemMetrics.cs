using System;
using System.Collections.Generic;
using System.Text;

namespace ScoutDataAgentMonitor.Domain.Models
{
    public class SystemMetrics
    {
        public double CpuUsage { get; set; }

        public double RamUsedMB { get; set; }

        public double RamTotalMB { get; set; }

        public double DiskUsedMB { get; set; }

        public double DiskTotalMB { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
