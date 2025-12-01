using UserService.API.ConfigurationExtensions;
using UserService.Application;
using UserService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddApiServices(configuration)
    .AddInfrastructureServices(configuration)
    .AddApplicationServices();

var app = builder.Build();

await app.ConfigureApp();

await app.RunAsync();