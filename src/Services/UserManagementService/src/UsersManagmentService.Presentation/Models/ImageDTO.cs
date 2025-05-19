using UsersManagementService.Common.Enums;

namespace UsersManagmentService.Presentation.Models;

public class ImageDto
{
    required public Guid Id { get; set; }
    required public Guid UserId { get; set; }
    required public string ImageUrl { get; set; }
    required public ImageType Type { get; set; }
}
