using RocketOps.Aspire.ServiceDefaults;
using RocketOps.Core.Infrastructure;
using RocketOps.Core.Infrastructure.Configurations;
using RocketOps.Monitoring.Api;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Host configuration
builder.Host.UseCoreInfrastructureHostServices(builder.Configuration);

// Add services
builder.Services.AddMonitoringApiServices(builder.Configuration);

var app = builder.Build();
// Configure middleware
app.MapDefaultEndpoints();
app.UseMonitoringApiServices();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure FastEndpoints and Swagger
app.UseFastEndpointsConfiguration(app.Environment);

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RocketOps Monitoring API v1");
    c.DefaultModelsExpandDepth(-1); // Force hide schemas
    c.DocExpansion(DocExpansion.List);
});

app.UseOpenApiConfiguration(builder.Configuration);

app.UseHttpsRedirection();

// Make sure this is called at application shutdown
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app.Run();