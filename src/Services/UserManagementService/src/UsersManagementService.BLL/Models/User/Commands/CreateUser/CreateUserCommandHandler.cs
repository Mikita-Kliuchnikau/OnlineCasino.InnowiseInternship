using MediatR;
using UsersManagementService.BLL.Interfaces;

namespace UsersManagementService.BLL.Models.User.Commands.CreateUser;

public class CreateUserCommandHandler(IUsersService usersService)
    : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        return await usersService.CreateUserAsync(request, cancellationToken);
    }
}