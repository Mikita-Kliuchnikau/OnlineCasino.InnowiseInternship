using NSubstitute;
using UsersManagementService.BLL.Models.Image.CreateImage;
using UsersManagementService.BLL.Models.Image.DeleteImage;
using UsersManagementService.BLL.Models.Image.UpdateImage;
using UsersManagementService.Common.Enums;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.UnitTests.TestEntities;

public static class ImageValidationTestEntities
{
    public static readonly CreateImageModel CreateModel = new(Guid.NewGuid(), Guid.NewGuid(), "url.com", ImageType.Default);
    public static readonly Guid DeleteModel = Guid.NewGuid();
    public static readonly UpdateImageModel UpdateModel = new(Guid.NewGuid(), Guid.NewGuid(), "url.com", ImageType.Default);

    public static readonly IImagesRepository _imagesRepositoryMock = Substitute.For<IImagesRepository>();
}