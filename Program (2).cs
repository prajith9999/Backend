using vueproject_asp.Repositories;

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

        // Register connection string from appsettings.json into DI container
        builder.Services.AddScoped(sp =>
            builder.Configuration.GetConnectionString("DefaultConnection"));

        // Register repositories for CRUD operations
        builder.Services.AddScoped<BodyRepository>();
        builder.Services.AddScoped<FaqRepository>();
        builder.Services.AddScoped<FeaturePageRepository>();
        builder.Services.AddScoped<FooterRepository>();
        builder.Services.AddScoped<PageContentRepository>();
        builder.Services.AddScoped<SocialMediaRepository>();
        builder.Services.AddScoped<SubscriptionRepository>();
        builder.Services.AddScoped<SubscriptionDetailsRepository>();
        builder.Services.AddScoped<UserRepository>();

        // Register AppHandler
        builder.Services.AddScoped<AppHandler>();

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
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
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
