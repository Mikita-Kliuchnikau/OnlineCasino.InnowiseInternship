using MediatR;

namespace UsersManagementService.BLL.Models.User.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<Guid> { }