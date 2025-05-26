using FluentValidation;
using UsersManagementService.BLL.Models.User;

namespace UsersManagementService.BLL.Interfaces.Validators;
public interface IUsersValidator 
{
    public IValidator<CreateUserModel> CreateUserModelValidator { get; }
    public IValidator<Guid> UserIdValidator { get; }
    public IValidator<UpdateUserModel> UpdateUserModelValidator { get; }
    public IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidator { get; }
}