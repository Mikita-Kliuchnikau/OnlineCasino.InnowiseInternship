using UsersManagementService.Common.Enums;

namespace UsersManagmentService.Presentation.Models;

public record ImageDTO(
    Guid Id,
    Guid UserId,
    string ImageUrl,
    ImageType Type);
