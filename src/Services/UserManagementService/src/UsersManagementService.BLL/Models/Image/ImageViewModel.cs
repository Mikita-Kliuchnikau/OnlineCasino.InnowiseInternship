using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.Image;

public record ImageViewModel(
    Guid Id,
    Guid UserId,
    string ImageUrl,
    ImageType Type);