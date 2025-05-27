using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.BLL.Models.Image.MappingConfigurations;

public static class ImagesMappingConfig
{
    public static void AddImagesMappingConfig(this IServiceCollection services)
    {
        TypeAdapterConfig<ImageModel, ImageEntity>.NewConfig()
            .Map(i => i.Id, src => src.Id)
            .Map(i => i.UserId, src => src.UserId)
            .Map(i => i.ImageUrl, src => src.ImageUrl)
            .Map(i => i.Type, src => src.Type);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}
