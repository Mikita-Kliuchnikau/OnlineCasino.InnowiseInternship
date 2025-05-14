using FluentValidation;
using UsersManagementService.BLL.Interfaces.Validators;
using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.UpdateUser;

namespace UsersManagementService.BLL.Models.User;

public class UsersValidator(
    IValidator<CreateUserModel> createUserModelValidator,
    IValidator<Guid> deleteUserValidator,
    IValidator<UpdateUserModel> updateUserModelValidator,
    IValidator<Guid> getUserValidator,
    IValidator<GetPagedUsersQuery> getPagedUsersQueryValidator) : IUsersValidator 
{
    public IValidator<CreateUserModel> CreateUserModelValidator { get; init; } = createUserModelValidator;
    public IValidator<Guid> DeleteUserValidator { get; init; } = deleteUserValidator;
    public IValidator<UpdateUserModel> UpdateUserModelValidator { get; init; } = updateUserModelValidator;
    public IValidator<Guid> GetUserValidator { get; init; } = getUserValidator;
    public IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidator { get; init; } = getPagedUsersQueryValidator;
}