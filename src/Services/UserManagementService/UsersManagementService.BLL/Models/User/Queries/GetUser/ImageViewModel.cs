using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.User.Queries.GetUser;

public record ImageViewModel(Guid Id,
    Guid UserId,
    string ImageUrl,
    ImageTypeEnum Type);