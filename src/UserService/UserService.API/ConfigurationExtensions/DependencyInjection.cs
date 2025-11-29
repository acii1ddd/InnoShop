using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UserService.API.EndpointsSettings;
using UserService.Domain.Enums;

namespace UserService.API.ConfigurationExtensions;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApiServices(IConfiguration config)
        {
            services
                .AddEndpoints(typeof(Program).Assembly)
                .AddOpenApiSpecs()
                .AddJwtAuthentication(config)
                .AddAuthorizationPolitics(config);
            
            return services;
        }
        
        private IServiceCollection AddOpenApiSpecs()
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, _, _) =>
                {
                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "Input your Bearer token to access this API",
                        In = ParameterLocation.Header,
                        Name = "Authorization"
                    });

                    document.SecurityRequirements.Add(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                    
                    document.Info = new OpenApiInfo
                    {
                        Title = "InnoShop UserService",
                        Version = "v1",
                        Description = "UserService API to manage users."
                    };

                    return Task.CompletedTask;
                });
            });

            return services;
        }
        
        private IServiceCollection AddJwtAuthentication(IConfiguration config)
        {
            var issuer = config["AuthSettings:Issuer"];
            var audience = config["AuthSettings:Audience"];
            var secret = config["AuthSettings:Secret"];
            
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(secret!)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

        private IServiceCollection AddAuthorizationPolitics(IConfiguration config)
        {
            services.AddAuthorization(authOptions =>
            {
                authOptions.AddPolicy
                (
                    "Admin",
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(
                            ClaimTypes.Role, 
                            // allowed roles
                            nameof(UserRole.Default), nameof(UserRole.Admin)
                        );
                    }
                );
            });

            return services;
        }
    }
}