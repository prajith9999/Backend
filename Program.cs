using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vueproject_asp.Repositories;

namespace vueproject_asp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add CORS policy to allow all origins, methods, and headers
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // Register connection string from appsettings.json into DI container (scoped for per-request use)
            builder.Services.AddScoped(sp =>
                builder.Configuration.GetConnectionString("DefaultConnection"));

            // Register repositories that use Dapper for data access
            builder.Services.AddScoped<BodyRepository>();    // Body repository
            builder.Services.AddScoped<UserRepository>();    // User repository
            builder.Services.AddScoped<FaqRepository>();     // Faq repository
            builder.Services.AddScoped<FeaturePageRepository>();  // FeaturePage repository
            builder.Services.AddScoped<FooterRepository>();   // Footer repository

            // Register SocialMediaRepository with connection string
            builder.Services.AddScoped<SocialMediaRepository>(provider =>
                new SocialMediaRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register PageContentRepository with connection string
            builder.Services.AddScoped<PageContentRepository>(provider =>
                new PageContentRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register SubscriptionRepository with connection string
            builder.Services.AddScoped<SubscriptionRepository>(provider =>
                new SubscriptionRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add controllers for API
            builder.Services.AddControllers();

            // Swagger for API documentation (Optional, can be removed in production)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Apply CORS policy globally
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline
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
