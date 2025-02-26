using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using RocketOps.Aspire.ServiceDefaults;
using RocketOps.Core.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Host configuration
builder.Host.UseCoreInfrastructureHostServices(builder.Configuration);

// Service registration - CALL ONLY ONCE
builder.Services.AddCoreInfrastructureServices(builder.Configuration);

// Configure Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddHttpClient();

var app = builder.Build();
app.MapDefaultEndpoints();
app.UseCoreInfrastructureServices();

await app.UseOcelot();

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

// Make sure this is called at application shutdown
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app.Run();