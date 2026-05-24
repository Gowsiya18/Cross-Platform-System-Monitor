using ScoutDataAgentMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoutDataAgentMonitor.Domain.Interfaces;

public interface IMonitorPlugin
{
    Task ExecuteAsync(SystemMetrics metrics);
}
