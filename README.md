# Cross-Platform-System-Monitor
A cross-platform desktop application in C# that monitors system resources (CPU, memory, disk usage) and supports a plugin system allowing for custom integrations

**Prerequisites**
.NET 10 SDK
Visual Studio 2022 or VS Code
Windows OS (for full CPU monitoring support)

**Run Steps**
git clone <your-repo-url>
cd ScoutDataAgentMonitor
dotnet restore
dotnet build
dotnet run

**Output Location**
Logs are generated at: /Logs/monitor.log

About the project: 
This application is designed using a Clean Architecture approach combined with a Plugin-based extensibility model. The core domain defines abstractions such as ISystemMonitor and IMonitorPlugin, ensuring that the monitoring logic is completely decoupled from infrastructure concerns like OS-specific performance tracking, file logging, or API communication.
The system follows the Dependency Inversion Principle, where the MonitoringService depends only on abstractions, not concrete implementations. This allows new plugins (e.g., Slack alerts, database logging, or cloud telemetry) to be added without modifying existing code. The monitoring layer is further abstracted using a strategy pattern to support cross-platform extensibility for Windows, Linux, and macOS.

**Architecture Pattern Used**
Clean Architecture + Plugin Architecture
ScoutDataAgentMonitor
│
├── Program.cs
│
├── Configuration
│   ├── AppSettings.cs
│
├── Domain
│   ├── Models
│   │   └── SystemMetrics.cs
│   │
│   └── Interfaces
│       ├── ISystemMonitor.cs
│       └── IMonitorPlugin.cs
│
├── Infrastructure
│   ├── Monitoring
│   │   └── WindowsSystemMonitor.cs
│   │
│   ├── Plugins
│   │   ├── FileLoggerPlugin.cs
│   │   └── ApiPosterPlugin.cs
│   │
│   └── Services
│       └── MonitoringService.cs
│
├── appsettings.json
│
└── Logs
    └── monitor.log
