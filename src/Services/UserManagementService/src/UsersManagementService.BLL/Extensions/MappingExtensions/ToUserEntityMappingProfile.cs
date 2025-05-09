using UsersManagementService.BLL.Models.User.Commands.CreateUser;
using UsersManagementService.BLL.Models.User.Commands.UpdateUser;
using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.BLL.Extensions.MappingExtensions;

public static class ToUserEntityMappingProfile
{
    public static UserEntity ToUserEntity(this CreateUserCommand createUserCommand)
    {
        return new UserEntity()
        {
            Id = createUserCommand.Id,
            AuthId = createUserCommand.AuthId,
            Username = createUserCommand.Username,
            Email = createUserCommand.Email
        };
    }

    public static UserEntity ToUserEntity(this UpdateUserCommand updateUserCommand)
    {
        return new UserEntity
        {
            Id = updateUserCommand.Id,
            AuthId = updateUserCommand.AuthId,
            Username = updateUserCommand.Username,
            Email = updateUserCommand.Email,
            Balance = updateUserCommand.Balance,
            VerificationStatus = updateUserCommand.VerificationStatus,
            IsBanned = updateUserCommand.IsBanned,
            IsDeleted = updateUserCommand.IsDeleted,
            FirstName = updateUserCommand.FirstName!,
            SecondName = updateUserCommand.SecondName,
            LastName = updateUserCommand.LastName,
            BirthDate = updateUserCommand.BirthDate,
            PassportNumber = updateUserCommand.PassportNumber,
            IdentificationNumber = updateUserCommand.IdentificationNumber
        };
    }
}