using MediatR;

namespace UsersManagementService.BLL.Models.Image.DeleteImage;

public record DeleteImageCommand(Guid Id) : IRequest<Guid> { }