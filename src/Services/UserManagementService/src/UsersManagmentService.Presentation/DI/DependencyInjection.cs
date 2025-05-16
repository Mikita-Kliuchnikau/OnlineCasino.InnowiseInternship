using UsersManagementService.BLL.DI;
using UsersManagmentService.Presentation.Models;

namespace UsersManagmentService.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBLL(configuration);
        services.AddDtoMappingConfig();

        return services;
    }
}
