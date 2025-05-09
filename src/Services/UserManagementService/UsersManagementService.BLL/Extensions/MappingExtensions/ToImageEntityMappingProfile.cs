using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagementService.DAL.Entites.Core;
namespace UsersManagementService.BLL.Extensions.MappingExtensions;

public static class ToImageEntityMappingProfile
{
    public static ImageEntity ToImageEntity(this CreateImageCommand createImageCommand)
    {
        return new ImageEntity
        {
            Id = createImageCommand.Id,
            UserId = createImageCommand.UserId,
            ImageUrl = createImageCommand.ImageUrl,
            Type = createImageCommand.Type
        };
    }

    public static ImageEntity ToImageEntity(this UpdateImageCommand updateImageCommand)
    {
        return new ImageEntity
        {
            Id = updateImageCommand.Id,
            UserId = updateImageCommand.UserId,
            ImageUrl = updateImageCommand.ImageUrl,
            Type = updateImageCommand.Type
        };
    }
}