using UsersManagementService.BLL.Models.User;
using UsersManagementService.Common.Enums;
using UsersManagementService.Presentation.Models;

namespace UsersManagementService.IntegrationTests.TestEntities;

public static class UserTestEntities
{
    public static readonly Guid BaseTestGuid = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
    public static readonly UserDto UserDto = new()
    {
        Id = BaseTestGuid,
        AuthId = BaseTestGuid,
        Username = "user",
        Email = "test@gmail.com",
        Balance = 199,
        VerificationStatus = VerificationStatus.Verified,
        IsBanned = false,
        FirstName = "FirstName",
        SecondName = "SecondName",
        LastName = "LastName"
    };
    public static readonly UserDto CreateUserDto = new()
    {
        Id = BaseTestGuid,
        AuthId = BaseTestGuid,
        Username = "user",
        Email = "test@gmail.com"
    };

    public static readonly PagedUsersViewModel userViewModelsResponse = new(1, 1, [   
        new UserViewModel(
                Id: BaseTestGuid,
                Username: "user",
                Email: "test@gmail.com",
                Balance: 0,
                VerificationStatus: VerificationStatus.UnVerified,
                IsBanned: false,
                IsDeleted: false,
                CreatedAt: DateTime.UtcNow)]);
}
