using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using vueproject_asp.Data;
using vueproject_asp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null) // Retry logic enabled
    )
);

builder.Services.AddControllers(); // Registering controllers for API

// Register repositories
builder.Services.AddScoped<BodyRepository>();  // Example of adding a repository service
builder.Services.AddScoped<UserRepository>();  // Add more repositories as needed

// Swagger for API documentation (Optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger in development
    app.UseSwaggerUI();  // Swagger UI
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Error handling for production
    app.UseHsts();  // HTTP Strict Transport Security
}

app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS
app.UseRouting();  // Enable routing for the application

app.MapControllers(); // Map the controller routes

app.Run(); // Start the application
