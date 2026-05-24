using System;
using System.Collections.Generic;
using System.Text;
namespace ScoutDataAgentMonitor.Configuration;

public class MonitoringSettings
{
    public int IntervalSeconds { get; set; }
}

public class ApiSettings
{
    public string Endpoint { get; set; } = string.Empty;

    public string LogFilePath { get; set; } = string.Empty;
}
