using FastEndpoints;
using Microsoft.OpenApi.Models;
using RocketOps.Core.Infrastructure;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Basic services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add explicit Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RocketOps API", Version = "v1" });
});

// Add FastEndpoints with minimal config
builder.Services.AddFastEndpoints();

// Add core services (reduced config)
builder.Services.AddCoreInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Enable detailed error page
app.UseDeveloperExceptionPage();

// Ensure the order is correct for middleware
app.UseRouting();
app.UseAuthorization();

// Explicitly configure Swagger (without FastEndpoints integration for now)
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RocketOps Alerts API v1");
    c.DefaultModelsExpandDepth(-1);
    c.DocExpansion(DocExpansion.List);
});

// Then use FastEndpoints
app.UseFastEndpoints();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();