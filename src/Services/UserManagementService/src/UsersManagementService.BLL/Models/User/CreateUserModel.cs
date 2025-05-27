namespace UsersManagementService.BLL.Models.User;

public record CreateUserModel(
    Guid Id, 
    Guid AuthId, 
    string Username, 
    string Email);