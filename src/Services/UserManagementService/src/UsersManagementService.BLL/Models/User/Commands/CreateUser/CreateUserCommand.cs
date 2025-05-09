using MediatR;

namespace UsersManagementService.BLL.Models.User.Commands.CreateUser;

public record CreateUserCommand(
    Guid Id, 
    Guid AuthId, 
    string Username, 
    string Email) : IRequest<Guid>{ }