using MediatR;
using UsersManagementService.BLL.Interfaces;

namespace UsersManagementService.BLL.Models.User.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUsersService usersService)
    : IRequestHandler<UpdateUserCommand, Guid>
{
    public async Task<Guid> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        return await usersService.UpdateUserAsync(request, cancellationToken);
    }
}