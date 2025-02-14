using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using LandWind.Repositories;
using LandWind.Interfaces;
using LandWind.Handlers;
using System;
using System.Data;
using LandWind.Handler;
using LandWind.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI setup for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Retrieve connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connection opened successfully!");
        // ... your database operations here ...
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error connecting to database: " + ex.Message);
    }
}
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("DefaultConnection is missing in the appsettings.json file.");
}

// Register SqlConnection with DI container as a scoped service
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

// Register Repository Interfaces and Implementations
builder.Services.AddScoped<IPageContentRepository, PageContentRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionDetailsRepository, SubscriptionDetailsRepository>();
builder.Services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
builder.Services.AddScoped<IBodyRepository, BodyRepository>();
builder.Services.AddScoped<IFeaturePageRepository, FeaturePageRepository>();
builder.Services.AddScoped<IFooterRepository, FooterRepository>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();

// Register Handlers (Scoped to ensure they are tied to the HTTP request)
builder.Services.AddScoped<IBodyHandler, BodyHandler>();
builder.Services.AddScoped<IFaqHandler, FaqHandler>();
builder.Services.AddScoped<IFeaturePageHandler, FeaturePageHandler>();
builder.Services.AddScoped<IFooterHandler, FooterHandler>();
builder.Services.AddScoped<IPageContentHandler, PageContentHandler>();
builder.Services.AddScoped<ISocialMediaHandler, SocialMediaHandler>();
builder.Services.AddScoped<ISubscriptionHandlerDetail, SubcriptionDetailsHandler>();
builder.Services.AddScoped<ISubscriptionHandler, SubscriptionHandler>();

// Add AutoMapper (if needed)
// builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;  // This will make Swagger UI accessible at root (optional)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

