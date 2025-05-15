using UsersManagementService.Common.Enums;
using UsersManagementService.DAL.Interfaces.Interceptors;

namespace UsersManagementService.DAL.Entites.Core;

public class ImageEntity : ISoftDeletable
{ 
    public Guid Id { get; set; }
    public UserEntity User { get; set; } = null!;
    public Guid UserId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public ImageType Type { get; set; } = ImageType.Default;
    public bool IsDeleted { get; set; } = false;
}