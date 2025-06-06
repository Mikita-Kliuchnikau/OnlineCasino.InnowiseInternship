using UsersManagementService.Common.Enums;

namespace UsersManagementService.BLL.Models.Image;

public record ImageViewModel(
    string ImageUrl,
    ImageType Type);