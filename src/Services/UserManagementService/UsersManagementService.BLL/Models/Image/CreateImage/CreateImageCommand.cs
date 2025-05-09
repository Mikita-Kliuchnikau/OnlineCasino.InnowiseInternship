using MediatR;
using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.Image.CreateImage;

public record CreateImageCommand(
    Guid Id,
    Guid UserId,
    string ImageUrl,
    ImageTypeEnum Type) : IRequest<Guid> { }