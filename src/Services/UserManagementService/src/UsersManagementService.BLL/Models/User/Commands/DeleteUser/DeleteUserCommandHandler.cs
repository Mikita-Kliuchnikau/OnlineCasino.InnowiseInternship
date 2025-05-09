using MediatR;
using UsersManagementService.BLL.Interfaces;

namespace UsersManagementService.BLL.Models.User.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUsersService usersService)
    : IRequestHandler<DeleteUserCommand, Guid>
{
    public async Task<Guid> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        return await usersService.DeleteUserAsync(request, cancellationToken);
    }
}