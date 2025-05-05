using System.ComponentModel.DataAnnotations.Schema;

namespace UsersManagementService.DAL.Entites;

public class ImageEntity
{ 
    public Guid Id { get; set; }
    public UserEntity User { get; set; }
    public Guid UserId { get; set; }
    public string ImagesUrl { get; set; }
    public ImageType Type { get; set; } = ImageType.Default;
}

public enum ImageType
{
    Default = 0,
    Passport = 1
}