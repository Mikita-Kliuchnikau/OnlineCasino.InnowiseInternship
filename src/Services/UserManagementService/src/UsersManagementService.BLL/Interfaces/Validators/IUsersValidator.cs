using FluentValidation;
using UsersManagementService.BLL.Models.User;

namespace UsersManagementService.BLL.Interfaces.Validators;
public interface IUsersValidator 
{
    public IValidator<CreateUserModel> CreateUserModelValidator { get; set; }
    public IValidator<Guid> UserIdValidator { get; set; }
    public IValidator<UpdateUserModel> UpdateUserModelValidator { get; set; }
    public IValidator<GetPagedUsersQuery> GetPagedUsersQueryValidator { get; set; }
}