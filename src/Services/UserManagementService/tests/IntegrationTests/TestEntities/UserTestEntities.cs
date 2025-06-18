using Microsoft.AspNetCore.Authentication.JwtBearer;
using UsersManagementService.BLL.Models.User;
using UsersManagementService.Common.Enums;
using UsersManagementService.Presentation.Models;

namespace UsersManagementService.IntegrationTests.TestEntities;

public static class UserTestEntities
{
    public static readonly string TokenType = JwtBearerDefaults.AuthenticationScheme;
    public static readonly string Token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImRXSGlXWWVwUFA1T0cwcWtVMHUtYyJ9.eyJpc3MiOiJodHRwczovL2Rldi1qMnprOHZqMTJ3N2Zlb3E1LnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJoTkZoUHNkUFhNQWJ3RFpZbWdWUGdlU0FiNEVjNjJZc0BjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyOC9hcGkvVXNlcnMiLCJpYXQiOjE3NTAyNDYwMTksImV4cCI6MTc1MDMzMjQxOSwic2NvcGUiOiJyZWFkOnVzZXJzIGNyZWF0ZTp1c2VyIGNyZWF0ZTppbWFnZSB1cGRhdGU6dXNlciBkZWxldGU6dXNlciBkZWxldGU6aW1hZ2UgYmFuOnVzZXIiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMiLCJhenAiOiJoTkZoUHNkUFhNQWJ3RFpZbWdWUGdlU0FiNEVjNjJZcyIsInBlcm1pc3Npb25zIjpbInJlYWQ6dXNlcnMiLCJjcmVhdGU6dXNlciIsImNyZWF0ZTppbWFnZSIsInVwZGF0ZTp1c2VyIiwiZGVsZXRlOnVzZXIiLCJkZWxldGU6aW1hZ2UiLCJiYW46dXNlciJdfQ.YYrI6q6cILptOZUcvKtbAh0-eR_1txf5exRj62z6oYVAzU0hE5GTVAPN7tSno0OglG9OOBFpFdnhxJMwdDpmnXbZ7_xa87Bbqsjvfr5ftgU_WqsYEh2swYiO9vQ0aH2sXsRH5BNH5H1J3eIKFd49mXrSiOzal6g622vc2u3wOAOoDnxX6fiWDT0bN62Iiu-tcyOtRVhGELMgV2ZlO5-9_jxmwouLfM-POUzP1C6VOUFVnHsmTzmbQa35ilS-UGWFzxs3iQD1pBxey4Fjq_1U7cEKDXDG0PNhUFvEh3oQ8m90r04gaWq167xDmPOJKtpFMSQ2mgnz7USMCc1isJLOCw";
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
