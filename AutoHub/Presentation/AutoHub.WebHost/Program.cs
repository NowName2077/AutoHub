using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper;
using AutoHub.Infrastructure.EntityFramework;
using AutoHub.WebHost.Helpers;
using AutoHub.Application.Services.Mapping;
using AutoHub.Application.Services;
using AutoHub.Application.Services.Abstractions;
using AutoHub.Domain.Repositories.Abstractions;
using AutoHub.Infrastructure.EntityFramework.RepositoriesEF;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration / DbContext
var connectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Connection string for ApplicationDbContext is not configured.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, npgsql =>
        npgsql.MigrationsAssembly("AutoHub.Infrastructure.EntityFramework")));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Controllers + FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoHub API", Version = "v1" });
});

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(ApplicationProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly, typeof(PresentationProfile).Assembly);


// Repositories
builder.Services.AddScoped<ICustomersRepository, CustomerRepository>();
builder.Services.AddScoped<ISellersRepository, SellerRepository>();
builder.Services.AddScoped<IListingsRepository, ListingsRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();

// Application services
builder.Services.AddScoped<ICustomersApplicationService, CustomersApplicationService>();
builder.Services.AddScoped<ISellersApplicationService, SellersApplicationService>();
builder.Services.AddScoped<IListingsApplicationService, ListingsApplicationService>();
builder.Services.AddScoped<IFavoritesApplicationService, FavoritesApplicationService>();
builder.Services.AddScoped<ITransactionsApplicationService, TransactionApplicationService>();

var app = builder.Build();

// Apply migrations (optional)
app.MigrateDatabase<ApplicationDbContext>();

// Middleware
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoHub API v1"));

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();