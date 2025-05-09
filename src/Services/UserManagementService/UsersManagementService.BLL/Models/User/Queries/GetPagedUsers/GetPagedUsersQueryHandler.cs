using MediatR;
using UsersManagementService.BLL.Interfaces;

namespace UsersManagementService.BLL.Models.User.Queries.GetPagedUsers;

public class GetPagedUsersQueryHandler(IUsersService usersService)
    : IRequestHandler<GetPagedUsersQuery, PagedUsersViewModel>
{
    public async Task<PagedUsersViewModel> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken)
    {
        return await usersService.GetPagedUserAsync(request, cancellationToken);
    }
}
