using MediatR;

namespace UsersManagementService.BLL.Models.User.Queries.GetPagedUsers;

public record GetPagedUsersQuery(int PageNumber, int PageSize) 
    : IRequest<PagedUsersViewModel> { }