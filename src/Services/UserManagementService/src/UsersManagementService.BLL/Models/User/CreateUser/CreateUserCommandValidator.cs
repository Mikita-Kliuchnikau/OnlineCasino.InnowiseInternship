using FluentValidation;
using UsersManagementService.BLL.Extensions;
using UsersManagementService.BLL.Resources;
using UsersManagementService.Common.Helpers;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.User.CreateUser;

public class CreateUserCommandValidator : AbstractValidator <CreateUserCommand>
{
    private static readonly ResourceHelper<UserMessages> resourceHelper = new(Common.Enums.CulturePreference.English);

    public CreateUserCommandValidator(IUsersRepository usersRepository)
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
        }).WithMessage(resourceHelper.GetValue(UserKeys.ValidationRequiredField));
    }
}