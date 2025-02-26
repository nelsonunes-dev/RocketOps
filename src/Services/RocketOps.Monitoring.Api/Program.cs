using RocketOps.Aspire.ServiceDefaults;
using RocketOps.Core.Infrastructure;
using RocketOps.Monitoring.Api;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Host configuration
builder.Host.UseCoreInfrastructureHostServices(builder.Configuration);

builder.Services.AddMonitoringApiServices(builder.Configuration);

var app = builder.Build();
app.MapDefaultEndpoints();

// Use API-specific middleware and services
app.UseMonitoringApiServices();

app.UseHttpsRedirection();

// Make sure this is called at application shutdown
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app.Run();