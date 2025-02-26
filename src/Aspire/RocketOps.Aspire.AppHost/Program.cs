var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.RocketOps_Gateway>("gateway");

builder.AddProject<Projects.Alerts_Api>("alerts-api");

builder.AddProject<Projects.RocketOps_Monitoring_Api>("monitoring-api");

builder.AddProject<Projects.Reporting_Api>("reporting-api");

builder.Build().Run();
