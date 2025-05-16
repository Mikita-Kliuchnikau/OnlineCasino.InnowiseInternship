using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.Image.UpdateImage;

public record UpdateImageModel(
    Guid Id, 
    Guid UserId, 
    string ImageUrl, 
    ImageType Type);