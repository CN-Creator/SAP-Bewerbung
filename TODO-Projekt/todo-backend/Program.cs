using Microsoft.EntityFrameworkCore;
using todo_backend.Interfaces;
using todo_backend.Models;
using todo_backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register MVC controllers
builder.Services.AddControllers();

// Register DbContext with in-memory database
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("TodoDB"));

// Register IUnitOfWork implementation for dependency injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline for development
if (app.Environment.IsDevelopment())
{
    // Enable Swagger
    app.UseSwagger();

    // Enable Swagger UI
    app.UseSwaggerUI();
}

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Setup Cors
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
});

// Enable authorization middleware
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Create a new scope for dependency injection
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // Get the required services
    var context = services.GetRequiredService<DataContext>();
    var unitOfWork = services.GetRequiredService<IUnitOfWork>();
}
catch (Exception ex)
{
    // Log any exceptions that occur during migration
    var logger = services.GetService<ILogger<Program>>();
    logger!.LogError(ex, "Error occurred during migration");
}

// Run the application
app.Run();
