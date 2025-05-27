using NSubstitute;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.Common.Enums;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.UnitTests.TestEntities;

public static class TestUserEntities
{
    public static readonly CreateUserModel CreateModel = new(Guid.NewGuid(), Guid.NewGuid(), "user", "User@email.com");
    public static readonly Guid DeleteModel = Guid.NewGuid();
    public static readonly UpdateUserModel UpdateModel = new(
        Id: Guid.NewGuid(), AuthId: Guid.NewGuid(), Username: "user", Email: "user@email.com", Balance: 0,
        VerificationStatus: VerificationStatus.UnVerified, IsBanned: false, FirstName: "user",
        SecondName: "user", LastName: "user", PassportNumber: "123456789", IdentificationNumber: "987654321");
    public static readonly Guid GetQuery = Guid.NewGuid();
    public static readonly GetPagedUsersQuery GetPagedQuery = new(1, 10);

    public static readonly IUsersRepository _usersRepositoryMock = Substitute.For<IUsersRepository>();
}
