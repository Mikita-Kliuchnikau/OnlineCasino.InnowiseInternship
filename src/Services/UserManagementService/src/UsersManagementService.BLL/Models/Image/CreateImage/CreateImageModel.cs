using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.Image.CreateImage;

public record CreateImageModel(
    Guid Id,
    Guid UserId,
    string ImageUrl,
    ImageType Type);