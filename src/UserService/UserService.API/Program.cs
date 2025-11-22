using UserService.API.ConfigurationExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

await app.ConfigureApp();

await app.RunAsync();