using EmployeePostTrace.BusinessLayer.Models;
using EmployeePostTrace.BusinessLayer.Services.Interfaces;
using EmployeePostTrace.BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.ComponentModel.Design;
using EmployeePostTrace.DataLayer.Repositories.Interfaces;
using EmployeePostTrace.DataLayer.Repositories;

namespace EmployeePostTrace.Api.Extentions;

public static class ProgrammExtention
{
    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "CRM", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Authorization: Bearer JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    Array.Empty<string>()
                },
            });
        });
    }

    public static void AddAuthentications(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = TokenOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = TokenOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = TokenOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ILetterRepository, LetterRepository>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ILetterService, LetterService>();
    }
}
