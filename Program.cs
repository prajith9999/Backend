using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vueproject_asp.Data;
using vueproject_asp.Repositories;
using Microsoft.Data.SqlClient;

namespace vueproject_asp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // Register the connection string from appsettings.json into DI container
            builder.Services.AddSingleton(sp =>
                builder.Configuration.GetConnectionString("DefaultConnection"));

            // Register DapperDbContext for handling SQL connection, make it scoped
            builder.Services.AddScoped<DapperDbContext>();

            // Register repositories that use Dapper for data access
            builder.Services.AddScoped<BodyRepository>();    // Add Body repository
            builder.Services.AddScoped<UserRepository>();    // Add User repository
            builder.Services.AddScoped<FaqRepository>();     // Add other repositories as necessary

            // Add controllers for API
            builder.Services.AddControllers();

            // Swagger for API documentation (Optional)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Apply CORS policy globally
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();  // Enable Swagger in development
                app.UseSwaggerUI();  // Swagger UI
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");  // Error handling for production
                app.UseHsts();  // HTTP Strict Transport Security
            }

            // Enforce HTTPS redirection and routing
            app.UseHttpsRedirection();
            app.UseRouting();

            // Map controllers
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
