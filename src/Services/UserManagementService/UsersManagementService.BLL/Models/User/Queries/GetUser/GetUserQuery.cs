using MediatR;

namespace UsersManagementService.BLL.Models.User.Queries.GetUser;

public record GetUserQuery(Guid Id) : IRequest<UserViewModel> { }