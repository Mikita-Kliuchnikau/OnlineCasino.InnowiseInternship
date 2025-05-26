using FluentValidation;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.DeleteUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.BLL.Models.User.UpdateUser;

namespace UsersManagementService.BLL.Models.User;

public class UsersValidator(
    IValidator<CreateUserModel> createUserModelValidator,
    DeleteUserValidator deleteUserValidator,
    IValidator<UpdateUserModel> updateUserModelValidator,
    GetUserValidator getUserValidator,
    IValidator<GetPagedUsersQuery> getPagedUsersQueryValidator) : IUsersValidator 
{
    public IValidator<CreateUserModel> CreateUserModelValidator { get; init; } = createUserModelValidator;
    public IValidator<Guid> DeleteUserValidator { get; init; } = deleteUserValidator;
    public IValidator<UpdateUserModel> UpdateUserModelValidator { get; init; } = updateUserModelValidator;
    public IValidator<Guid> GetUserValidator { get; init; } = getUserValidator;
    public IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidator { get; init; } = getPagedUsersQueryValidator;
}