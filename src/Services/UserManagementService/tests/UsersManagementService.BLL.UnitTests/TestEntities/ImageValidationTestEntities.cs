using NSubstitute;
using UsersManagementService.BLL.Models.Image;
using UsersManagementService.BLL.Validators.ImagesValidators;
using UsersManagementService.Common.Enums;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.BLL.UnitTests.TestEntities;

public static class ImageValidationTestEntities
{
    public static readonly ImageModel ImageModel = new(Guid.NewGuid(), Guid.NewGuid(), "url.com", ImageType.Default);
    public static readonly Guid DeleteModel = Guid.NewGuid();

    public static readonly IImagesRepository _imagesRepositoryMock = Substitute.For<IImagesRepository>();
}