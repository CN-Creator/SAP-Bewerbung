using API.Data;
using API.Data.Models;
using API.Extensions;
using API.Interfaces;
using API.JobSystem;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins(app.Configuration.GetValue<string>("AllowedHosts")));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var unitOfWork = services.GetRequiredService<IUnitOfWork>();
    var cache = services.GetRequiredService<IMemoryCache>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "Error occurred during migration");
}

await SchedulerHandler.StartScheduler();
//TODO hier muss die ID von unserer Paulmann LED übergeben werden
await SchedulerHandler.AddLEDJob("0x00158d00084fb69a");

app.Run();
//in case this code is reached, close scheduler and all running jobs
await SchedulerHandler.CloseScheduler();
