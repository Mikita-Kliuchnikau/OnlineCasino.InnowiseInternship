using FluentAssertions;
using NSubstitute;
using UsersManagementService.BLL.Services;
using UsersManagementService.DAL.Entites.Core;
using static UsersManagementService.BLL.UnitTests.TestEntities.ImagesServiceTestsEntities;

namespace UsersManagementService.BLL.UnitTests;

public class ImagesServiceTests
{
    [Fact]
    public async Task CreateImageAsync_CallsService_ReturnsImageId()
    {
        //Arrange
        _imagesRepositoryMock.CreateAsync(Arg.Any<ImageEntity>(), default)
            .Returns(CreateCommand.Id);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.CreateImageAsync(CreateCommand, default);

        //Assert
        result.Should().Be(CreateCommand.Id);
    }

    [Fact]
    public async Task CreateImageAsync_InvalidImage_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidCommand = CreateCommand with { Id = Guid.Empty };
        _imagesRepositoryMock.CreateAsync(Arg.Any<ImageEntity>(), default)
            .Returns(invalidCommand.Id);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.CreateImageAsync(invalidCommand, default);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteImageAsync_CallsService_ReturnsImageId()
    {
        //Arrange
        _imagesRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .Returns(DeleteCommand.Id);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.DeleteImageAsync(DeleteCommand, default);

        //Assert
        result.Should().Be(DeleteCommand.Id);
    }

    [Fact]
    public async Task DeleteImageAsync_InvalidImage_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidCommand = DeleteCommand with { Id = Guid.Empty };
        _imagesRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .Returns(invalidCommand.Id);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.DeleteImageAsync(invalidCommand, default);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task UpdateImageAsync_CallsService_ReturnsImageId()
    {
        //Arrange
        _imagesRepositoryMock.UpdateAsync(Arg.Any<ImageEntity>(), default)
            .Returns(UpdateCommand.Id);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.UpdateImageAsync(UpdateCommand, default);

        //Assert
        result.Should().Be(UpdateCommand.Id);
    }

    [Fact]
    public async Task UpdateImageAsync_InvalidImage_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidCommand = UpdateCommand with { Id = Guid.Empty };
        _imagesRepositoryMock.UpdateAsync(Arg.Any<ImageEntity>(), default)
            .Returns(invalidCommand.Id);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.UpdateImageAsync(invalidCommand, default);

        //Assert
        result.Should().BeEmpty();
    }
}
