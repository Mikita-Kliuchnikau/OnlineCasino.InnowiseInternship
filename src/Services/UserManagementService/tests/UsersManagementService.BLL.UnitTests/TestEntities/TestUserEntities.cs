using Microsoft.Extensions.Logging;
using NSubstitute;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.BLL.Services;
using UsersManagementService.Common.Enums;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.UnitTests.TestEntities;

public static class TestUserEntities
{
    public static readonly Guid BaseGuid = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
    public static readonly CreateUserModel CreateModel = new("123", "user", "User@email.com");
    public static readonly Guid DeleteModel = BaseGuid;
    public static readonly UpdateUserModel UpdateModel = new(
        Id: BaseGuid, AuthId: "123", Username: "user", Email: "user@email.com", Balance: 0,
        VerificationStatus: VerificationStatus.UnVerified, FirstName: "user",
        SecondName: "user", LastName: "user", PassportNumber: "123456789", IdentificationNumber: "987654321");
    public static readonly Guid GetQuery = BaseGuid;
    public static readonly GetPagedUsersQuery GetPagedQuery = new(1, 10);

    public static readonly ILogger<UsersService> _loggerMock = Substitute.For<ILogger<UsersService>>();
    public static readonly IUsersRepository _usersRepositoryMock = Substitute.For<IUsersRepository>();
}
