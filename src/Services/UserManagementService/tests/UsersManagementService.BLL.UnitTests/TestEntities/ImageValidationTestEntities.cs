using NSubstitute;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.DeleteImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagementService.Common.Enums;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.UnitTests.TestEntities;

public static class ImageValidationTestEntities
{
    public static readonly CreateImageCommand CreateCommand = new(Guid.NewGuid(), Guid.NewGuid(), "url.com", ImageType.Default);
    public static readonly DeleteImageCommand DeleteCommand = new(Guid.NewGuid());
    public static readonly UpdateImageCommand UpdateCommand = new(Guid.NewGuid(), Guid.NewGuid(), "url.com", ImageType.Default, false);

    public static IImagesRepository _imagesRepositoryMock = Substitute.For<IImagesRepository>();
}