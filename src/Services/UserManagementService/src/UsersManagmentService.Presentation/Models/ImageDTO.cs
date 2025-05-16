using UsersManagementService.Common.Enums;

namespace UsersManagmentService.Presentation.Models;

public record ImageDto(
    Guid Id,
    Guid UserId,
    string ImageUrl,
    ImageType Type);
