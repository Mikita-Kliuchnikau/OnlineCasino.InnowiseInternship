namespace UsersManagementService.BLL.Models.User.CreateUser;

public record CreateUserModel(
    Guid Id, 
    Guid AuthId, 
    string Username, 
    string Email);