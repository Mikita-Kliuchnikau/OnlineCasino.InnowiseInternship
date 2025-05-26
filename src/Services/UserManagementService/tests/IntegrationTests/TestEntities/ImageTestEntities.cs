using UsersManagementService.Common.Enums;
using UsersManagementService.Presentation.Models;
using static UsersManagementService.IntegrationTests.TestEntities.UserTestEntities;

namespace UsersManagementService.IntegrationTests.TestEntities;

public static class ImageTestEntities
{
    public static readonly ImageDto ImageDto = new()
    {
        Id = BaseTestGuid,
        UserId = BaseTestGuid,
        ImageUrl = "https://example.com/image.jpg",
        Type = ImageType.Default
    };
}
