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
            .Returns(CreateModel.Id);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.CreateImageAsync(CreateModel, default);

        //Assert
        result.Should().Be(CreateModel.Id);
    }

    [Fact]
    public async Task CreateImageAsync_InvalidImage_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidModel = CreateModel with { Id = Guid.Empty };
        _imagesRepositoryMock.CreateAsync(Arg.Any<ImageEntity>(), default)
            .Returns(invalidModel.Id);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.CreateImageAsync(invalidModel, default);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteImageAsync_CallsService_ReturnsImageId()
    {
        //Arrange
        _imagesRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .Returns(DeleteModel);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.DeleteImageAsync(DeleteModel, default);

        //Assert
        result.Should().Be(DeleteModel);
    }

    [Fact]
    public async Task DeleteImageAsync_InvalidImage_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidModel = Guid.Empty;
        _imagesRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .Returns(invalidModel);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.DeleteImageAsync(invalidModel, default);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task UpdateImageAsync_CallsService_ReturnsImageId()
    {
        //Arrange
        _imagesRepositoryMock.UpdateAsync(Arg.Any<ImageEntity>(), default)
            .Returns(UpdateModel.Id);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.UpdateImageAsync(UpdateModel, default);

        //Assert
        result.Should().Be(UpdateModel.Id);
    }

    [Fact]
    public async Task UpdateImageAsync_InvalidImage_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidModel = UpdateModel with { Id = Guid.Empty };
        _imagesRepositoryMock.UpdateAsync(Arg.Any<ImageEntity>(), default)
            .Returns(invalidModel.Id);

        var service = new ImagesService(_imagesRepositoryMock);

        //Act
        var result = await service.UpdateImageAsync(invalidModel, default);

        //Assert
        result.Should().BeEmpty();
    }
}
