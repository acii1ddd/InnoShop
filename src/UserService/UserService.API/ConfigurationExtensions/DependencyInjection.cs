using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UserService.API.EndpointsSettings;

namespace UserService.API.ConfigurationExtensions;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApiServices(IConfiguration config)
        {
            services
                .AddOpenApiSpec()
                .AddEndpoints(typeof(Program).Assembly)
                .AddAuthentication();

            // jwt settings
            var issuer = config["AuthSettings:Issuer"];
            var audience = config["AuthSettings:Audience"];
            var lifetime = int.Parse(config["AuthSettings:Lifetime"] ?? "60");
            var secret = config["AuthSettings:Secret"];
            
            // config auth
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
            
            // services.AddAuthorization(authOptions =>
            // {
            //     authOptions.AddPolicy
            //     (
            //         "Default, Admin",
            //         policy =>
            //         {
            //             policy.RequireAuthenticatedUser();
            //             policy.RequireClaim(
            //                 ClaimTypes.Role, 
            //                 nameof(UserRole.Default), 
            //                 nameof(UserRole.Admin)
            //             );
            //         }
            //     );
            //
            //     authOptions.AddPolicy
            //     (
            //         nameof(UserRole.Admin),
            //         policy =>
            //         {
            //             policy.RequireAuthenticatedUser();
            //             policy.RequireClaim(
            //                 ClaimTypes.Role, 
            //                 nameof(UserRole.Admin)
            //             );
            //         }
            //     );
            // });
            
            return services;
        }

        private IServiceCollection AddOpenApiSpec()
        {
            services.AddOpenApi();
    
            return services;
        }
    }
}