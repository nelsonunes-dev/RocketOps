using Serilog;
using ServiceDefaults;
using Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Host.UseShareInfrastructureHostServices(builder.Configuration);
builder.Services.AddSharedInfrastructureServices(builder.Configuration);

var app = builder.Build();
app.MapDefaultEndpoints();
app.UseSharedInfrastructureServices(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

// Make sure this is called at application shutdown
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app.Run();
