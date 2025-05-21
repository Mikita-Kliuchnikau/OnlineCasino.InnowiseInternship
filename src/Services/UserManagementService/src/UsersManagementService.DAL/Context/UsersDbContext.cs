using Microsoft.EntityFrameworkCore;
using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.DAL.Context;

public class UsersDbContext : DbContext
{
    public UsersDbContext() { }
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
}