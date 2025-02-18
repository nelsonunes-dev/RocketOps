var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Gateway>("gateway");

builder.AddProject<Projects.Alerts_Api>("alerts-api");

builder.AddProject<Projects.Monitoring_Api>("monitoring-api");

builder.AddProject<Projects.Reporting_Api>("reporting-api");

builder.Build().Run();
