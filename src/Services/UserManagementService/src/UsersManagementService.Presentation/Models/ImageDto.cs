using UsersManagementService.Common.Enums;

namespace UsersManagementService.Presentation.Models;

public class ImageDto
{
    required public Guid UserId { get; set; }
    required public ImageType Type { get; set; }
    required public IFormFile File { get; set; }
}
