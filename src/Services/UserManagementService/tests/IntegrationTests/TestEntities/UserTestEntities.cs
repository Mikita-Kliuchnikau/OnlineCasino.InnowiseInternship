using Microsoft.AspNetCore.Authentication.JwtBearer;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.Common.Enums;
using UsersManagementService.Presentation.Models;

namespace UsersManagementService.IntegrationTests.TestEntities;

public static class UserTestEntities
{
    public static readonly Guid BaseTestGuid = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
    public static readonly UpdateUserDto UpdateUserDto = new()
    {
        AuthId = "123",
        Username = "user",
        Email = "test@gmail.com",
        Balance = 199,
        VerificationStatus = VerificationStatus.Verified,
        FirstName = "FirstName",
        SecondName = "SecondName",
        LastName = "LastName"
    };
    public static readonly CreateUserDto CreateUserDto = new()
    {
        AuthId = "123",
        Username = "user",
        Email = "test@gmail.com"
    };

    public static readonly PagedUsersViewModel userViewModelsResponse = new(1, 1, [   
        new UserViewModel(
                Id: BaseTestGuid,
                AuthId: "123",
                Username: "user",
                Email: "test@gmail.com",
                Balance: 0,
                VerificationStatus: VerificationStatus.UnVerified,
                IsBanned: false,
                CreatedAt: DateTime.UtcNow)]);
}
