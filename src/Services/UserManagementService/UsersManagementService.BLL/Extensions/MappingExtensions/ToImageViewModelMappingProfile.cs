using UsersManagementService.BLL.Models.User.Queries.GetUser;
using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.BLL.Extensions.MappingExtensions;

public static class ToImageViewModelMappingProfile
{
    public static ImageViewModel ToImageViewModel(this ImageEntity imageEntity)
    {
        return new(imageEntity.Id, imageEntity.UserId, imageEntity.ImageUrl, imageEntity.Type);
    }
}