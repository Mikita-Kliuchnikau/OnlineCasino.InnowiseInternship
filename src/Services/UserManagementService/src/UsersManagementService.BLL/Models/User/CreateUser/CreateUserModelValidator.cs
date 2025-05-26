using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.BLL.Resources;
using UsersManagementService.Common.Helpers;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.User.CreateUser;

public class CreateUserModelValidator : AbstractValidator <CreateUserModel>
{
    private static readonly ResourceHelper<UserMessages> resourceHelper = new(Common.Enums.CulturePreference.English);

    public CreateUserModelValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u.Id)
            .BaseIdRules();
        RuleFor(u => u.AuthId)
            .BaseIdRules();
        RuleFor(u => u.Username)
            .BaseNamesRules();
        RuleFor(u => u.Email)
            .BaseEmailRules();
        RuleFor(u => u)
            .MustAsync(async (User, CancellationToken) =>
        {
            return await usersRepository
            .IsUserUniqeAsync(
                id: User.Id,
                authId: User.AuthId,
                username: User.Username,
                email: User.Email,
                cancellationToken: CancellationToken);
        }).WithMessage(resourceHelper.GetValue(UserKeys.ValidationNotUniqueUser));
    }
}