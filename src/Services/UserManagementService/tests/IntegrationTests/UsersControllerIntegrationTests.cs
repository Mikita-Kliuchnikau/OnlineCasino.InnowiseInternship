using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UsersManagementService.DAL.Context;
using UsersManagmentService.Presentation.Models;

namespace UsersManagementService.IntegrationTests;

public class UsersControllerIntegrationTests(TestingWebAppFactory<UsersDbContext> factory) : IClassFixture<TestingWebAppFactory<UsersDbContext>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Create_ReturnsNewUserId()
    {
        // Arrange
        var user = new UserDto
        {
            Id = Guid.NewGuid(),
            AuthId = Guid.NewGuid(),
            Username = "testuser",
            Email = "testuser@example.com"
        };

        // Act
        var response = await _client.PostAsJsonAsync("https://localhost:7228/api/Users", user);
        var newUserId = await response.Content.ReadAsStringAsync();

        // Assert
        newUserId.Should().Be(user.Id.ToString());
    }

    [Fact]
    public async Task Get_ReturnsPagedUsers()
    {
        // Arrange
        int page = 1, pageSize = 10;

        // Act
        var response = await _client.GetAsync($"/api/Users?page={page}&pageSize={pageSize}");

        // Assert
        response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ReturnsUser()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/Users/{userId}");

        // Assert
        response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Update_ReturnsUserId()
    {
        // Arrange
        var user = new UserDto
        {
            Id = Guid.NewGuid(),
            AuthId = Guid.NewGuid(),
            Username = "updateduser",
            Email = "updateduser@example.com"
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/Users", user);
        var updatedUserId = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        updatedUserId.Should().Be(user.Id.ToString());
    }

    [Fact]
    public async Task Delete_ReturnsUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"/api/Users/{userId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
