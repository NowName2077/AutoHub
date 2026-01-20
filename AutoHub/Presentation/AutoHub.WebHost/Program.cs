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
using AutoHub.WebHost.Mapping;
using FluentValidation.AspNetCore;

namespace AutoHub.WebHost;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string for ApplicationDbContext is not configured.");
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, npgsql =>
                npgsql.MigrationsAssembly("AutoHub.Infrastructure.EntityFramework")));

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoHub API", Version = "v1" });
        });
        
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        
        builder.Services.AddAuthorization();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(cfg => { }, typeof(ApplicationProfile).Assembly, typeof(PresentationProfile).Assembly);

        builder.Services.AddScoped<ICustomersRepository, CustomerRepository>();
        builder.Services.AddScoped<ISellersRepository, SellerRepository>();
        builder.Services.AddScoped<IListingsRepository, ListingsRepository>();
        builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();

        builder.Services.AddScoped<ICustomersApplicationService, CustomersApplicationService>();
        builder.Services.AddScoped<ISellersApplicationService, SellersApplicationService>();
        builder.Services.AddScoped<IListingsApplicationService, ListingsApplicationService>();
        builder.Services.AddScoped<IFavoritesApplicationService, FavoritesApplicationService>();
        builder.Services.AddScoped<ITransactionsApplicationService, TransactionApplicationService>();

        var app = builder.Build();

        app.MigrateDatabase<ApplicationDbContext>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.MigrateDatabase<ApplicationDbContext>();

        app.Run();
    }
}