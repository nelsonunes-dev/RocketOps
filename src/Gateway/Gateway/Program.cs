using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using ServiceDefaults;
using Shared.Infrastructure;
using Shared.Infrastructure.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Host.UseShareInfrastructureHostServices(builder.Configuration);
builder.Services.AddSharedInfrastructureServices(builder.Configuration);

// Configure Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddHttpClient();

// Configure health checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddCheck<MicroservicesHealthCheck>("microservices");

var app = builder.Build();
app.MapDefaultEndpoints();
app.UseSharedInfrastructureServices(builder.Configuration);
await app.UseOcelot();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

// Make sure this is called at application shutdown
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app.Run();
