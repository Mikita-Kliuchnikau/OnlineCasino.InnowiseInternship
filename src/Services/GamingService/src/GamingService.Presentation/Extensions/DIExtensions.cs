using GamingService.Presentation.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace GamingService.Presentation.Extensions;

public static class DIExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "UsersManagementService", Version = "v1.0.0" });

            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "Using the Authorization header with the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            };

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                { securitySchema, [JwtBearerDefaults.AuthenticationScheme] }
            };
            c.AddSecurityRequirement(securityRequirement);
        });
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services)
    {
        var authOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<Auth0Options>>().Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = $"https://{authOptions.Domain}";
            options.Audience = authOptions.Audience;
            options.RequireHttpsMetadata = false;
        });
        return services;
    }
}
