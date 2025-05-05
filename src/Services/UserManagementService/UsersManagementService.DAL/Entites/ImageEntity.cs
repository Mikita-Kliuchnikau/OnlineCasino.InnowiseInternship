using System.ComponentModel.DataAnnotations.Schema;

namespace UsersManagementService.DAL.Entites;

public class ImageEntity
{ 
    public Guid Id { get; set; }
    public UserEntity User { get; set; }
    public Guid UserId { get; set; }
    public List<string> ImagesUrl { get; set; } = []; 
}
