using UsersManagementService.Common.Enums;

namespace UsersManagmentService.Presentation.Models;

public class ImageDto
{
    required public Guid Id;
    required public Guid UserId;
    required public string ImageUrl;
    required public ImageType Type;
}
