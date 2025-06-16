using UsersManagementService.BLL.Models.User;
using UsersManagementService.Common.Enums;
using UsersManagementService.Presentation.Models;

namespace UsersManagementService.IntegrationTests.TestEntities;

public static class UserTestEntities
{
    public static readonly string TokenType = "Bearer";
    public static readonly string Token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImRXSGlXWWVwUFA1T0cwcWtVMHUtYyJ9.eyJpc3MiOiJodHRwczovL2Rldi1qMnprOHZqMTJ3N2Zlb3E1LnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJoTkZoUHNkUFhNQWJ3RFpZbWdWUGdlU0FiNEVjNjJZc0BjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyOC9hcGkvVXNlcnMiLCJpYXQiOjE3NTAwNzM0NDQsImV4cCI6MTc1MDE1OTg0NCwic2NvcGUiOiJyZWFkOnVzZXJzIGNyZWF0ZTp1c2VyIGNyZWF0ZTppbWFnZSB1cGRhdGU6dXNlciBkZWxldGU6dXNlciBkZWxldGU6aW1hZ2UgYmFuOnVzZXIiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMiLCJhenAiOiJoTkZoUHNkUFhNQWJ3RFpZbWdWUGdlU0FiNEVjNjJZcyIsInBlcm1pc3Npb25zIjpbInJlYWQ6dXNlcnMiLCJjcmVhdGU6dXNlciIsImNyZWF0ZTppbWFnZSIsInVwZGF0ZTp1c2VyIiwiZGVsZXRlOnVzZXIiLCJkZWxldGU6aW1hZ2UiLCJiYW46dXNlciJdfQ.QSTQIZQygD7Jk02LJ8Lp0VnWz3agYsq6R2oxQVsIHQECTCbJoBGNB1SOHWFTChCeZfUoEaRtwzNRV1EeXblbU14Mg9dQzjfYTbtj7mPtBU0ZPn6AMO9w6P5CEYA8KZBipdpx8Cfmmt7s5ZjSNgxUFODS-spq5HAzIxWadKOx7vlnHY_v-PsNe_ehB7y2K92lPbD4WcBt1lEy6lPeyHsHsb4YrmcHkO2juj9Tcb2_ry3MQ7Xxveq5xgD6xXl5s8ftfTi1bFlbG7zvT5NGVtrOAVgpVO110bqUClogTgTmi8a5gWus491kh-WKauS6TalvD4n-VPxMLotviZnOXB2PtQ";
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
