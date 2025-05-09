using FluentValidation;
using UsersManagementService.BLL.Constants;
using UsersManagementService.BLL.Extensions.ValidatingExtentions;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.Models.User.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator <CreateUserCommand>
{
    public CreateUserCommandValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u.Id)
            .CommonIdRules();
        RuleFor(u => u.AuthId)
            .CommonIdRules();
        RuleFor(u => u.Username)
            .CommonNamesRules();
        RuleFor(u => u.Email)
            .CommonEmailRules();
        RuleFor(u => new { u.Id, u.AuthId, u.Username, u.Email })
            .MustAsync(async (User, CancellationToken) =>
        {
            return await usersRepository
            .IsUserUniqeAsync(
                id: User.Id,
                authId: User.AuthId,
                username: User.Username,
                email: User.Email,
                cancellationToken: CancellationToken);
        }).WithMessage(ValidationMessages.ValidationNotUniqueUser);
        // Should a more specific feedback be added?
    }
}