using FastEndpoints;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using RocketOps.Core.Infrastructure;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Basic services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add explicit Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RocketOps Gateway API", Version = "v1" });
});

// Add FastEndpoints
builder.Services.AddFastEndpoints();

// Add Ocelot
builder.Services.AddOcelot(builder.Configuration);

// Add core services
builder.Services.AddCoreInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Enable detailed error page in development
app.UseDeveloperExceptionPage();

// Ensure the order is correct for middleware
app.UseRouting();
app.UseAuthorization();

// Configure Swagger
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RocketOps Gateway API v1");
    c.DefaultModelsExpandDepth(-1);
    c.DocExpansion(DocExpansion.List);
});

// Configure FastEndpoints
app.UseFastEndpoints();

// Ocelot must be configured AFTER Swagger for Gateway API documentation to work
app.UseOcelot().Wait();

app.UseHttpsRedirection();

app.Run();