using MediatR;
using UsersManagementService.BLL.Interfaces;

namespace UsersManagementService.BLL.Models.User.Queries.GetUser;

public class GetUserQueryHandler(IUsersService usersService)
    : IRequestHandler<GetUserQuery, UserViewModel>
{
    public async Task<UserViewModel> Handle(
        GetUserQuery request, 
        CancellationToken cancellationToken)
    {
        return await usersService.GetUserByIdAsync(request, cancellationToken);
    }
}