using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.BLL.Models.Image;

public static class ImagesMappingConfig
{
    public static void AddImagesMappingConfig(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateImageCommand, ImageEntity>.NewConfig()
            .Map(i => i.Id, src => src.Id)
            .Map(i => i.UserId, src => src.UserId)
            .Map(i => i.ImageUrl, src => src.ImageUrl)
            .Map(i => i.Type, src => src.Type);

        TypeAdapterConfig<UpdateImageCommand, ImageEntity>.NewConfig()
            .Map(i => i.Id, src => src.Id)
            .Map(i => i.UserId, src => src.UserId)
            .Map(i => i.ImageUrl, src => src.ImageUrl)
            .Map(i => i.Type, src => src.Type)
            .Map(i => i.IsDeleted, src => src.IsDeleted);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}
