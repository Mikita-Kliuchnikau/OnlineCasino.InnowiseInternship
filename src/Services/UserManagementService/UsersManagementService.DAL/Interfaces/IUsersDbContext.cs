using Microsoft.EntityFrameworkCore;
using UsersManagementService.DAL.Entites;

namespace UsersManagementService.DAL.Interfaces;

public interface IUsersDbContext
{
    DbSet<UserEntity> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
