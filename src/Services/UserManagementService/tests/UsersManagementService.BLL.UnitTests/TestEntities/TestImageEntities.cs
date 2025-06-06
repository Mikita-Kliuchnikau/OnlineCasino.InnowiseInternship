using Microsoft.Extensions.Logging;
using NSubstitute;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.BLL.Services;
using UsersManagementService.Common.Enums;
using UsersManagementService.DAL.Interfaces.Repositories;
using UsersManagementService.DAL.Interfaces.Services;

namespace UsersManagementService.BLL.UnitTests.TestEntities;

public static class TestImageEntities
{
    public static readonly ImageModel ImageModel = new(Guid.NewGuid(), Guid.NewGuid(), ImageType.Default, Stream.Null, "image/png");
    public static readonly Guid DeleteModel = Guid.NewGuid();

    public static readonly ILogger<ImagesService> _loggerMock = Substitute.For<ILogger<ImagesService>>();
    public static readonly IAzureBlobService _blobServiceMock = Substitute.For<IAzureBlobService>();
    public static readonly IImagesRepository _imagesRepositoryMock = Substitute.For<IImagesRepository>();
}
