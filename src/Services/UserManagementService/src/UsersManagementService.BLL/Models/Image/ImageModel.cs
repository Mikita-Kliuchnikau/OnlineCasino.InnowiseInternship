using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.Image;

public record ImageModel(
    Guid Id,
    Guid UserId,
    ImageType Type,
    Stream Stream,
    string ContentType);