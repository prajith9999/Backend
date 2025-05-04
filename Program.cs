using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LandWind.Repositories;
using LandWind.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register Handlers for CRUD operations
builder.Services.AddScoped<IBodyHandler, BodyHandler>();
builder.Services.AddScoped<IPageContentHandler, PageContentHandler>();
builder.Services.AddScoped<IFeaturePageHandler, FeaturePageHandler>();
builder.Services.AddScoped<IFaqHandler, FaqHandler>();
builder.Services.AddScoped<IFooterHandler, FooterHandler>();
builder.Services.AddScoped<ISubscriptionHandler, SubscriptionHandler>();

// Register Repository Interfaces and Implementations
builder.Services.AddScoped<IAppRepository, AppRepository>();

// Register OpenAPI (Swagger) support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper (if required for DTO mapping)
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enable OpenAPI/Swagger in development
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authorization middleware (if applicable)
app.UseAuthorization();

// Register the controllers
app.MapControllers();

app.Run();
