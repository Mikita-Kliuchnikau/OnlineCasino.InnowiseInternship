namespace UsersManagementService.BLL.Models.User;

public record CreateUserModel(
    string AuthId, 
    string Username, 
    string Email);