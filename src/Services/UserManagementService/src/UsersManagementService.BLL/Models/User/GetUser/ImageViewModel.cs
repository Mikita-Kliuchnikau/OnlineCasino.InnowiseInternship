using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.User.GetUser;

public record ImageViewModel(Guid Id,
    Guid UserId,
    string ImageUrl,
    ImageType Type);