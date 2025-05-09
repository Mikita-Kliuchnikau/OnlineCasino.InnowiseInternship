using MediatR;
using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.Image.UpdateImage;

public record UpdateImageCommand(
    Guid Id, 
    Guid UserId, 
    string ImageUrl, 
    ImageType Type, 
    bool IsDeleted) : IRequest<Guid> { }