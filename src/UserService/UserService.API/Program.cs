using User.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();

var app = builder.Build();

app.ConfigureApp();

await app.RunAsync();