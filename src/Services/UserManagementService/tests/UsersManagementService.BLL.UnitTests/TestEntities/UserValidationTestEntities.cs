using NSubstitute;
using UsersManagementService.BLL.Models.User.CreateUser;
using UsersManagementService.BLL.Models.User.DeleteUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.BLL.Models.User.UpdateUser;
using UsersManagementService.Common.Enums;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.UnitTests.TestEntities;

public static class UserValidationTestEntities
{
    public static readonly CreateUserCommand CreateCommand = new(Guid.NewGuid(), Guid.NewGuid(), "user", "User@email.com");
    public static readonly DeleteUserCommand DeleteCommand = new(Guid.NewGuid());
    public static readonly UpdateUserCommand UpdateCommand = new(
        Id: Guid.NewGuid(), AuthId: Guid.NewGuid(), Username: "user", Email: "user@email.com", Balance: 0,
        VerificationStatus: VerificationStatus.UnVerified, IsBanned: false, IsDeleted: false, FirstName: "user",
        SecondName: "user", LastName: "user", PassportNumber: "123456789", IdentificationNumber: "987654321");
    public static readonly GetUserQuery GetQuery = new(Guid.NewGuid());
    public static readonly GetPagedUsersQuery GetPagedQuery = new(1, 10);

    public static readonly IUsersRepository _usersRepositoryMock = Substitute.For<IUsersRepository>();
}
