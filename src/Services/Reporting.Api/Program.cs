using RocketOps.Aspire.ServiceDefaults;
using RocketOps.Core.Infrastructure;
using RocketOps.Core.Infrastructure.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Host configuration
builder.Host.UseCoreInfrastructureHostServices(builder.Configuration);

// Service registration - ONCE only
builder.Services.AddCoreInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();

// Add FastEndpoints and OpenAPI
builder.Services.AddFastEndpointsConfiguration(builder.Configuration);
builder.Services.AddOpenApiConfiguration(builder.Configuration);

var app = builder.Build();
app.MapDefaultEndpoints();

// Use infrastructure middleware
app.UseCoreInfrastructureServices();

// Use FastEndpoints and OpenAPI
app.UseFastEndpointsConfiguration(app.Environment);
app.UseOpenApiConfiguration(app.Configuration);

app.UseHttpsRedirection();

// Make sure this is called at application shutdown
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app.Run();
