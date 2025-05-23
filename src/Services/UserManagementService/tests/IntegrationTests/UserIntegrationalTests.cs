using FluentAssertions;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using UsersManagementService.BLL.Models.User.GetUser;
using UsersManagementService.BLL.Models.User.GetPagedUsers;
using static UsersManagementService.IntegrationTests.TestEntities.UserTestEntities;
using static UsersManagementService.IntegrationTests.TestEntities.ImageTestEntities;

namespace UsersManagementService.IntegrationTests;

public class UserIntegrationalTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>, IAsyncLifetime
{
    public async Task InitializeAsync() => await Task.CompletedTask;

    public async Task DisposeAsync() => await factory.ResetDatabaseAsync();

    [Fact]
    public async Task CreateUser_ValidUser_ReturnsId()
    {
        // Arrange
        var httpResponse = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, UserDto);
        
        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.Should().Be(UserDto.Id);
    }

    [Fact]
    public async Task CreateUser_UserAlreadyExists_ReturnsInternalServerError()
    {
        // Arrange
        var _ = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, UserDto);

        // Act
        var httpResponse = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, UserDto);

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task UpdateUser_ValidUser_ReturnsId()
    { 
        // Arrange
        var _ = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, CreateUserDto);
        var httpResponse = await factory.HttpClient.PutAsJsonAsync(BaseUserUrl, UserDto);

        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.Should().Be(CreateUserDto.Id);
    }

    [Fact]
    public async Task UpdateUser_UserDoesntExist_ReturnsInternalServerError()
    {
        // Arrange
        // Act
        var httpResponse = await factory.HttpClient.PutAsJsonAsync(BaseUserUrl, UserDto);

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task DeleteUser_ValidId_ReturnsId()
    {
        // Arrange
        var _ = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, UserDto);
        var httpResponse = await factory.HttpClient.DeleteAsync(BaseUserUrl + $"/{BaseTestGuid}");

        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.Should().Be(BaseTestGuid);
    }

    [Fact]
    public async Task DeleteUser_UserDoesntExist_ReturnsInternalServerError()
    {
        // Arrange
        // Act
        var httpResponse = await factory.HttpClient.DeleteAsync(BaseUserUrl + $"/{BaseTestGuid}");

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task GetUserById_ValidId_ReturnUserViewModel()
    {
        // Arrange
        var _ = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, UserDto);
        var httpResponse = await factory.HttpClient.GetAsync(BaseUserUrl + $"/{ BaseTestGuid }");

        // Act
        var Response = (await httpResponse.Content.ReadFromJsonAsync<UserViewModel>(factory.JsonSerializerOptions))!;

        // Assert
        Response.Id.Should().Be(BaseTestGuid);
        Response.Email.Should().Be(UserDto.Email);
        Response.Username.Should().Be(UserDto.Username);
    }

    [Fact]
    public async Task GetUserById_UserDoesntExist_ReturnsInternalServerError()
    {
        // Arrange
        // Act
        var httpResponse = await factory.HttpClient.GetAsync(BaseUserUrl + $"/{BaseTestGuid}");

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task GetUsers_ValidRequest_ReturnsPagedUsersViewModel()
    {
        // Arrange
        var _ = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, UserDto);
        var httpResponse = await factory.HttpClient.GetAsync(BaseUserUrl + $"?page=1&pageSize=1");

        // Act
        var Response = (await httpResponse.Content.ReadFromJsonAsync<PagedUsersViewModel>(factory.JsonSerializerOptions))!;

        // Assert
        Response.PageNumber.Should().Be(userViewModelsResponse.PageNumber);
        Response.TotalCount.Should().Be(userViewModelsResponse.TotalCount);
        Response.UserViewModels?.First().Id.Should().Be(userViewModelsResponse.UserViewModels!.First().Id);
        Response.UserViewModels?.First().Username.Should().Be(userViewModelsResponse.UserViewModels!.First().Username);
        Response.UserViewModels?.First().Email.Should().Be(userViewModelsResponse.UserViewModels!.First().Email);
    }

    [Fact]
    public async Task CreateImage_ValidImage_ReturnsId()
    {
        // Arrange
        var _ = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, UserDto);
        var httpResponse = await factory.HttpClient.PostAsJsonAsync(BaseImageUrl, ImageDto);

        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.Should().Be(BaseTestGuid);
    }

    [Fact]
    public async Task CreateImage_ImageAlreadyExists_ReturnsInternalServerError()
    {
        // Arrange
        var _ = await factory.HttpClient.PostAsJsonAsync(BaseImageUrl, ImageDto);

        // Act
        var httpResponse = await factory.HttpClient.PostAsJsonAsync(BaseImageUrl, ImageDto);

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task UpdateImage_ValidImageReturnsId()
    {
        // Arrange
        var _ = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, UserDto);
        _ = await factory.HttpClient.PostAsJsonAsync(BaseImageUrl, ImageDto);
        var httpResponse = await factory.HttpClient.PutAsJsonAsync(BaseImageUrl, ImageDto);
        
        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.Should().Be(BaseTestGuid);
    }

    [Fact]
    public async Task UpdateImage_ImageDoesntExists_ReturnsInternalServerError()
    {
        // Arrange
        // Act
        var httpResponse = await factory.HttpClient.PutAsJsonAsync(BaseImageUrl, ImageDto);

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task DeleteImage_ValidId_ReturnId()
    {
        // Arrange
        var _ = await factory.HttpClient.PostAsJsonAsync(BaseUserUrl, UserDto);
        _ = await factory.HttpClient.PostAsJsonAsync(BaseImageUrl, ImageDto);
        var httpResponse = await factory.HttpClient.DeleteAsync(BaseImageUrl + $"/{BaseTestGuid}");
        
        // Act
        var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Guid>();

        // Assert
        apiResponse.Should().Be(BaseTestGuid);
    }

    [Fact]
    public async Task DeleteImage_ImageDoesntExist_ReturnsInternalServerError()
    {
        // Arrange
        // Act
        var httpResponse = await factory.HttpClient.DeleteAsync(BaseImageUrl + $"/{BaseTestGuid}");

        // Assert
        httpResponse.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }
}
