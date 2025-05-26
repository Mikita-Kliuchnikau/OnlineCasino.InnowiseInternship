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
    public async Task CreateUserAsync_CallsService_ReturnsUserId()
    {
        //Arrange
        _usersRepositoryMock.CreateAsync(Arg.Any<UserEntity>(), default)
            .Returns(CreateModel.Id);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.CreateUserAsync(CreateModel, default);

        //Assert
        result.Should().Be(CreateModel.Id);
    }

    [Fact]
    public async Task CreateUserAsync_InvalidUser_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidModel = CreateModel with { Id = Guid.Empty };
        _usersRepositoryMock.CreateAsync(Arg.Any<UserEntity>(), default)
            .Returns(invalidModel.Id);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.CreateUserAsync(invalidModel, default);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteUserAsync_CallsService_ReturnsUserId()
    {
        //Arrange
        _usersRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .Returns(DeleteModel);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.DeleteUserAsync(DeleteModel, default);

        //Assert
        result.Should().Be(DeleteModel);
    }

    [Fact]
    public async Task DeleteUserAsync_InvalidUser_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidModel = Guid.Empty;
        _usersRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .Returns(invalidModel);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.DeleteUserAsync(invalidModel, default);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task UpdateUserAsync_CallsService_ReturnsUserId()
    {
        //Arrange
        _usersRepositoryMock.UpdateAsync(Arg.Any<UserEntity>(), default)
            .Returns(UpdateModel.Id);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.UpdateUserAsync(UpdateModel, default);

        //Assert
        result.Should().Be(UpdateModel.Id);
    }

    [Fact]
    public async Task UpdateUserAsync_InvalidUser_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidModel = UpdateModel with { Id = Guid.Empty };
        _usersRepositoryMock.UpdateAsync(Arg.Any<UserEntity>(), default)
            .Returns(invalidModel.Id);

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.UpdateUserAsync(invalidModel, default);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetUserByIdAsync_CallsService_ReturnsUser()
    {
        //Arrange
        _usersRepositoryMock.GetByIdAsync(Arg.Any<Guid>(), default)
            .Returns(new UserEntity { Id = GetQuery, AuthId = Guid.NewGuid() });

        var service = new UsersService(_usersRepositoryMock);

        //Act
        var result = await service.GetUserByIdAsync(GetQuery, default);

        //Assert
        result.Id.Should().Be(GetQuery);
    }

    [Fact]
    public async Task GetUserByIdAsync_InvalidUser_CallsService_ThrowNotFoundException()
    {
        //Arrange
        var invalidQuery = Guid.Empty;
        _usersRepositoryMock.GetByIdAsync(Arg.Any<Guid>(), default)
            .ThrowsAsync(new NotFoundException("", null!));
        var service = new UsersService(_usersRepositoryMock);

        //Act
        Func<Task> act = async () => await service.GetUserByIdAsync(invalidQuery, default);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
