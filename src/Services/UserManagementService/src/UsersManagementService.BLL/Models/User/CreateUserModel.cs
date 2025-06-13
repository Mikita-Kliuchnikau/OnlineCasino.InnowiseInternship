namespace UsersManagementService.BLL.Models.User;

public record CreateUserModel(
    Guid AuthId, 
    string Username, 
    string Email);