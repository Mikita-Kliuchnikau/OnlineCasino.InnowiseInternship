using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.Image;

public record ImageModel(
    Guid UserId,
    ImageType Type,
    Stream Stream,
    string ContentType);