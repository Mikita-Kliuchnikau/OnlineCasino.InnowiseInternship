using FluentAssertions;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using UsersManagementService.BLL.Models.User;
using static UsersManagementService.IntegrationTests.TestEntities.UserTestEntities;
using static UsersManagementService.IntegrationTests.TestEntities.ImageTestEntities;
using static UsersManagementService.IntegrationTests.Constants.EndpointsUrls;
using UsersManagementService.Common.Enums;
using System.Net.Http.Headers;

namespace UsersManagementService.IntegrationTests;

public class UserIntegrationalTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>, IAsyncLifetime
{
    public async Task InitializeAsync() => await Task.CompletedTask;

    public async Task DisposeAsync() => await factory.ResetDatabaseAsync();

    [Fact]
    public async Task CreateUser_ValidUser_ReturnsId()
    {
        // Arrange
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
        var httpResponse = await client.PostAsJsonAsync(BaseUserUrl, CreateUserDto);
        
        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.GetType().Should().Be(typeof(Guid));
    }

    [Fact]
    public async Task CreateUser_UserAlreadyExists_ReturnsInternalServerError()
    {
        // Arrangevar
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
        var _ = await client.PostAsJsonAsync(BaseUserUrl, CreateUserDto);

        // Act
        var httpResponse = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, CreateUserDto);

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task UpdateUser_ValidUser_ReturnsId()
    { 
        // Arrange
        var response = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, CreateUserDto);
        var guid = await response.Content.ReadFromJsonAsync<Guid>();
        var UpdateUserDto = new Presentation.Models.UpdateUserDto 
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
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
        var httpResponse = await client.PutAsJsonAsync(BaseUserUrl + $"/{guid}", UpdateUserDto);

        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.GetType().Should().Be(typeof(Guid));
    }

    [Fact]
    public async Task UpdateUser_UserDoesntExist_ReturnsInternalServerError()
    {
        // Arrange
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);

        // Act
        var httpResponse = await client.PutAsJsonAsync(BaseUserUrl + $"/{BaseTestGuid}", UpdateUserDto);

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task DeleteUser_ValidId_ReturnsId()
    {
        // Arrange
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
        var response = await client.PostAsJsonAsync(BaseUserUrl, CreateUserDto);
        var guid = await response.Content.ReadFromJsonAsync<Guid>();
        var httpResponse = await client.DeleteAsync(BaseUserUrl + $"/{guid}");

        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.Should().Be(guid);
    }

    [Fact]
    public async Task DeleteUser_UserDoesntExist_ReturnsInternalServerError()
    {
        // Arrange
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);

        // Act
        var httpResponse = await client.DeleteAsync(BaseUserUrl + $"/{BaseTestGuid}");

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task GetUserById_ValidId_ReturnUserViewModel()
    {
        // Arrange
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
        var response = await client.PostAsJsonAsync(BaseUserUrl, CreateUserDto);
        var guid = await response.Content.ReadFromJsonAsync<Guid>();
        var httpResponse = await client.GetAsync(BaseUserUrl + $"/{guid}");

        // Act
        var Response = (await httpResponse.Content.ReadFromJsonAsync<UserViewModel>(factory.JsonSerializerOptions))!;

        // Assert
        Response.Id.GetType().Should().Be(typeof(Guid));
        Response.Email.Should().Be(CreateUserDto.Email);
        Response.Username.Should().Be(CreateUserDto.Username);
    }

    [Fact]
    public async Task GetUserById_UserDoesntExist_ReturnsInternalServerError()
    {
        // Arrange
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);

        // Act
        var httpResponse = await client.GetAsync(BaseUserUrl + $"/{BaseTestGuid}");

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task GetUsers_ValidRequest_ReturnsPagedUsersViewModel()
    {
        // Arrange
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
        var response = await client.PostAsJsonAsync(BaseUserUrl, CreateUserDto);
        var guid = await response.Content.ReadFromJsonAsync<Guid>();
        var httpResponse = await client.GetAsync(BaseUserUrl + $"?page=1&pageSize=1");

        // Act
        var Response = (await httpResponse.Content.ReadFromJsonAsync<PagedUsersViewModel>(factory.JsonSerializerOptions))!;

        // Assert
        Response.PageNumber.Should().Be(userViewModelsResponse.PageNumber);
        Response.TotalCount.Should().Be(userViewModelsResponse.TotalCount);
        Response.UserViewModels?.First().Id.Should().Be(guid);
        Response.UserViewModels?.First().Username.Should().Be(CreateUserDto.Username);
        Response.UserViewModels?.First().Email.Should().Be(CreateUserDto.Email);
    }

    [Fact]
    public async Task CreateImage_ValidImage_ReturnsId()
    {
        // Arrange
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
        var response = await client.PostAsJsonAsync(BaseUserUrl, CreateUserDto);
        var guid = await response.Content.ReadFromJsonAsync<Guid>();
        var ImageRequest = new MultipartFormDataContent()
        {
            { new StringContent(guid.ToString()), "UserId" },
            { new StringContent(((int) BaseImageDto.Type).ToString()), "Type" },
            { new StreamContent(BaseImageDto.File.OpenReadStream()), "File", BaseImageDto.File.FileName }
        };
        var httpResponse = await client.PostAsync(BaseImageUrl, ImageRequest);

        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.GetType().Should().Be(typeof(Guid));
    }

    [Fact]
    public async Task DeleteImage_ValidId_ReturnId()
    {
        // Arrange
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);
        var response = await client.PostAsJsonAsync(BaseUserUrl, CreateUserDto);
        var guid = await response.Content.ReadFromJsonAsync<Guid>();
        var ImageRequest = new MultipartFormDataContent()
        {
            { new StringContent(guid.ToString()), "UserId" },
            { new StringContent(((int) BaseImageDto.Type).ToString()), "Type" },
            { new StreamContent(BaseImageDto.File.OpenReadStream()), "File", BaseImageDto.File.FileName }
        };
        response = await client.PostAsync(BaseImageUrl, ImageRequest);
        guid = await response.Content.ReadFromJsonAsync<Guid>();
        var httpResponse = await client.DeleteAsync(BaseImageUrl + $"/{guid}");
        
        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.Should().Be(guid);
    }

    [Fact]
    public async Task DeleteImage_ImageDoesntExist_ReturnsInternalServerError()
    {
        // Arrange
        var client = factory.HttpClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenType, Token);

        // Act
        var httpResponse = await client.DeleteAsync(BaseImageUrl + $"/{BaseTestGuid}");

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }
}
