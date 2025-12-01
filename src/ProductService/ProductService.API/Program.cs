using ProductService.API.ConfigurationExtensions;
using ProductService.Application;
using ProductService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddApiServices(configuration)
    .AddInfrastructureServices(configuration)
    .AddApplicationServices();

var app = builder.Build();

await app.ConfigureApp();

await app.RunAsync();