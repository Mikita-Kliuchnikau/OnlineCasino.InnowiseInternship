namespace UsersManagementService.BLL.Models.User.CreateUser;

public record CreateUserCommand(
    Guid Id, 
    Guid AuthId, 
    string Username, 
    string Email);