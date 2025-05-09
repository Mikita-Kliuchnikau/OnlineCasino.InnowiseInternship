using MediatR;
using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.Image.CreateImage;

public record CreateImageCommand(
    Guid Id,
    Guid UserId,
    string ImageUrl,
    ImageType Type) : IRequest<Guid> { }