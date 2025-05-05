using Microsoft.EntityFrameworkCore;
using UsersManagementService.DAL.Entites;

namespace UsersManagementService.DAL.Context;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
}
