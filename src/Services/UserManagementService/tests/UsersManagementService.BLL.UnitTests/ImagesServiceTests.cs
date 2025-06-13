using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UsersManagementService.BLL.Services;
using UsersManagementService.Common.Exceptions;
using UsersManagementService.DAL.Entites.Core;
using static UsersManagementService.BLL.UnitTests.TestEntities.TestImageEntities;

namespace UsersManagementService.BLL.UnitTests;

public class ImagesServiceTests
{
    [Fact]
    public async Task CreateImage_ValidImage_ReturnsImageId()
    {
        //Arrange
        _imagesRepositoryMock.CreateAsync(Arg.Any<ImageEntity>(), default)
            .Returns(BaseGuid);

        _blobServiceMock.UploadImageAsync(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>(), default)
            .Returns("https://example.com/image.jpg");

        var service = new ImagesService(_imagesRepositoryMock, _blobServiceMock,  _loggerMock);

        //Act
        var result = await service.CreateImageAsync(ImageModel, default);

        //Assert
        result.Should().Be(BaseGuid);
    }

    [Fact]
    public async Task DeleteImage_ValidId_ReturnsImageId()
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
    public async Task DeleteImage_InvalidId_ThrowNotFoundException()
    {
        //Arrange
        var invalidModel = Guid.Empty;
        _imagesRepositoryMock.DeleteAsync(Arg.Any<Guid>(), default)
            .ThrowsAsync(new NotFoundException("", null!));

        _blobServiceMock.UploadImageAsync(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>(), default)
            .Returns("https://example.com/image.jpg");

        var service = new ImagesService(_imagesRepositoryMock, _blobServiceMock, _loggerMock);

        //Act
        Func<Task> result = async () => await service.DeleteImageAsync(invalidModel, default);

        //Assert
        await result.Should().ThrowAsync<NotFoundException>();
    }
}
