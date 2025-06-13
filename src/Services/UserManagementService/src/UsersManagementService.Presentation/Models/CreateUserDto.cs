namespace UsersManagementService.Presentation.Models;

public class CreateUserDto
{
    required public Guid AuthId { get; set; }
    required public string Username { get; set; }
    required public string Email { get; set; }
}
