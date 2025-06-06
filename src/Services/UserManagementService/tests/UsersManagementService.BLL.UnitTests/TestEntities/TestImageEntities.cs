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
    public static readonly Guid BaseGuid = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
    public static readonly ImageModel ImageModel = new(BaseGuid, ImageType.Default, Stream.Null, "image/png");
    public static readonly Guid DeleteModel = BaseGuid;

    public static readonly ILogger<ImagesService> _loggerMock = Substitute.For<ILogger<ImagesService>>();
    public static readonly IAzureBlobService _blobServiceMock = Substitute.For<IAzureBlobService>();
    public static readonly IImagesRepository _imagesRepositoryMock = Substitute.For<IImagesRepository>();
}
