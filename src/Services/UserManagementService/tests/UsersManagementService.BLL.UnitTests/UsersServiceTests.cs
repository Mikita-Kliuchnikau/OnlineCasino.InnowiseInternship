using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UsersManagementService.BLL.Services;
using UsersManagementService.Common.Exceptions;
using UsersManagementService.DAL.Entites.Core;
using static UsersManagementService.BLL.UnitTests.TestEntities.TestUserEntities;

namespace UsersManagementService.BLL.UnitTests;

public class UsersServiceTests
{
    [Fact]
    public async Task CreateUser_ValidUser_ReturnsUserId()
    {
        //Arrange
        var response = Guid.NewGuid();
        _usersRepositoryMock.CreateAsync(Arg.Any<UserEntity>(), default)
            .Returns(response);

        var service = new UsersService(_usersRepositoryMock, _loggerMock);

        //Act
        var result = await service.CreateUserAsync(CreateModel, default);

        //Assert
        result.Should().Be(response);
    }

    [Fact]
    public async Task DeleteUser_ValidId_ReturnsUserId()
    {
        //Arrange
        _usersRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .Returns(DeleteModel);

        var service = new UsersService(_usersRepositoryMock, _loggerMock);

        //Act
        var result = await service.DeleteUserAsync(DeleteModel, default);

        //Assert
        result.Should().Be(DeleteModel);
    }

    [Fact]
    public async Task DeleteUser_InvalidId_ThrowNotFoundException()
    {
        //Arrange
        var invalidModel = Guid.Empty;
        _usersRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .ThrowsAsync(new NotFoundException("", null!));

        var service = new UsersService(_usersRepositoryMock, _loggerMock);

        //Act
        Func<Task> result = async () => await service.DeleteUserAsync(invalidModel, default);

        //Assert
        await result.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task UpdateUserAsync_ValidUser_ReturnsUserId()
    {
        //Arrange
        _usersRepositoryMock.UpdateAsync(Arg.Any<UserEntity>(), default)
            .Returns(UpdateModel.Id);

        var service = new UsersService(_usersRepositoryMock, _loggerMock);

        //Act
        var result = await service.UpdateUserAsync(UpdateModel, default);

        //Assert
        result.Should().Be(UpdateModel.Id);
    }

    [Fact]
    public async Task UpdateUserAsync_InvalidUser_ThrowNotFoundException()
    {
        //Arrange
        var invalidModel = UpdateModel with { Id = Guid.Empty };
        _usersRepositoryMock.UpdateAsync(Arg.Any<UserEntity>(), default)
            .ThrowsAsync(new NotFoundException("", null!));

        var service = new UsersService(_usersRepositoryMock, _loggerMock);

        //Act
        Func<Task> result = async () => await service.UpdateUserAsync(invalidModel, default);

        //Assert
        await result.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task GetUserByIdAsync_ValidId_ReturnsUser()
    {
        //Arrange
        _usersRepositoryMock.GetByIdAsync(Arg.Any<Guid>(), default)
            .Returns(new UserEntity { Id = GetQuery, AuthId = "123" });

        var service = new UsersService(_usersRepositoryMock, _loggerMock);

        //Act
        var result = await service.GetUserByIdAsync(GetQuery, default);

        //Assert
        result.Id.Should().Be(GetQuery);
    }

    [Fact]
    public async Task GetUserByIdAsync_InvalidId_ThrowNotFoundException()
    {
        //Arrange
        var invalidQuery = Guid.Empty;
        _usersRepositoryMock.GetByIdAsync(Arg.Any<Guid>(), default)
            .ThrowsAsync(new NotFoundException("", null!));
        var service = new UsersService(_usersRepositoryMock, _loggerMock);

        //Act
        Func<Task> result = async () => await service.GetUserByIdAsync(invalidQuery, default);

        //Assert
        await result.Should().ThrowAsync<NotFoundException>();
    }
}
