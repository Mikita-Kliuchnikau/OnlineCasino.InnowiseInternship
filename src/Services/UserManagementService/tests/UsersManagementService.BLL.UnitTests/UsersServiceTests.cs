using FluentAssertions;
using NSubstitute;
using UsersManagementService.BLL.Services;
using UsersManagementService.DAL.Entites.Core;
using static UsersManagementService.BLL.UnitTests.TestEntities.UsersServiceTestEntities;

namespace UsersManagementService.BLL.UnitTests;

public class UsersServiceTests
{
    [Fact]
    public async Task CreateUserAsync_CallsService_ReturnsUserId()
    {
        //Arrange
        _usersRepositoryMock.CreateAsync(Arg.Any<UserEntity>(), default)
            .Returns(CreateCommand.Id);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.CreateUserAsync(CreateCommand, default);

        //Assert
        result.Should().Be(CreateCommand.Id);
    }

    [Fact]
    public async Task CreateUserAsync_InvalidUser_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidCommand = CreateCommand with { Id = Guid.Empty };
        _usersRepositoryMock.CreateAsync(Arg.Any<UserEntity>(), default)
            .Returns(invalidCommand.Id);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.CreateUserAsync(invalidCommand, default);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteUserAsync_CallsService_ReturnsUserId()
    {
        //Arrange
        _usersRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .Returns(DeleteCommand.Id);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.DeleteUserAsync(DeleteCommand, default);

        //Assert
        result.Should().Be(DeleteCommand.Id);
    }

    [Fact]
    public async Task DeleteUserAsync_InvalidUser_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidCommand = DeleteCommand with { Id = Guid.Empty };
        _usersRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .Returns(invalidCommand.Id);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.DeleteUserAsync(invalidCommand, default);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task UpdateUserAsync_CallsService_ReturnsUserId()
    {
        //Arrange
        _usersRepositoryMock.UpdateAsync(Arg.Any<UserEntity>(), default)
            .Returns(UpdateCommand.Id);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.UpdateUserAsync(UpdateCommand, default);

        //Assert
        result.Should().Be(UpdateCommand.Id);
    }

    [Fact]
    public async Task UpdateUserAsync_InvalidUser_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidCommand = UpdateCommand with { Id = Guid.Empty };
        _usersRepositoryMock.UpdateAsync(Arg.Any<UserEntity>(), default)
            .Returns(invalidCommand.Id);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.UpdateUserAsync(invalidCommand, default);

        //Assert
        result.Should().BeEmpty();
    }
}
