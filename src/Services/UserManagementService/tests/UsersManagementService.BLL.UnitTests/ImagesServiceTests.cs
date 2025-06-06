using FluentAssertions;
using NSubstitute;
using UsersManagementService.BLL.Services;
using UsersManagementService.DAL.Entites.Core;
using static UsersManagementService.BLL.UnitTests.TestEntities.TestImageEntities;

namespace UsersManagementService.BLL.UnitTests;

public class ImagesServiceTests
{
    [Fact]
    public async Task CreateImageAsync_CallsService_ReturnsImageId()
    {
        //Arrange
        _imagesRepositoryMock.CreateAsync(Arg.Any<ImageEntity>(), default)
            .Returns(ImageModel.Id);

        _blobServiceMock.UploadImageAsync(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>(), default)
            .Returns("https://example.com/image.jpg");

        var service = new ImagesService(_imagesRepositoryMock, _blobServiceMock,  _loggerMock);

        //Act
        var result = await service.CreateImageAsync(ImageModel, default);

        //Assert
        result.Should().Be(ImageModel.Id);
    }

    [Fact]
    public async Task CreateImageAsync_InvalidImage_CallsService_ReturnsNull()
    {
        //Arrange
        var invalidModel = ImageModel with { Id = Guid.Empty };
        _imagesRepositoryMock.CreateAsync(Arg.Any<ImageEntity>(), default)
            .Returns(invalidModel.Id);

        _blobServiceMock.UploadImageAsync(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>(), default)
            .Returns("https://example.com/image.jpg");

        var service = new ImagesService(_imagesRepositoryMock, _blobServiceMock, _loggerMock);

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

        _blobServiceMock.UploadImageAsync(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>(), default)
            .Returns("https://example.com/image.jpg");

        var service = new ImagesService(_imagesRepositoryMock, _blobServiceMock, _loggerMock);

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

        _blobServiceMock.UploadImageAsync(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>(), default)
            .Returns("https://example.com/image.jpg");

        var service = new ImagesService(_imagesRepositoryMock, _blobServiceMock, _loggerMock);

        //Act
        var result = await service.DeleteImageAsync(invalidModel, default);

        //Assert
        result.Should().BeEmpty();
    }
}
