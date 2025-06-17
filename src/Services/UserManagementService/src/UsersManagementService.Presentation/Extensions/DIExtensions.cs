using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using UsersManagementService.Presentation.AuthScopes;
using UsersManagementService.Presentation.Options;

namespace UsersManagementService.Presentation.Extensions;

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
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                { securitySchema, [ "Bearer" ] }
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
            .GetRequiredService<IOptions<Auth0Options>>();

        services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Authority = authOptions.Value.Domain;
                options.Audience = authOptions.Value.Audience;
            });
        return services;
    }

    public static IServiceCollection AddAuthorizationPolicies(
        this IServiceCollection services)
    {
        var authOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<Auth0Options>>();

        services.AddAuthorizationBuilder()
            .AddPolicy("ban:users", policy =>
                policy.Requirements.Add(new HasScopeRequirement("ban:users", authOptions.Value.Domain)));
        return services;
    }
}
