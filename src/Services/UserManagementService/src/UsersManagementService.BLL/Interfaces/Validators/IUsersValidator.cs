using FluentValidation;
using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.UpdateUser;

namespace UsersManagementService.BLL.Interfaces.Validators;
public interface IUsersValidator 
{
    public IValidator<CreateUserModel> CreateUserModelValidator { get; init; }
    public IValidator<Guid> DeleteUserValidator { get; init; }
    public IValidator<UpdateUserModel> UpdateUserModelValidator { get; init; }
    public IValidator<Guid> GetUserValidator { get; init; }
    public IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidator { get; init; }
}