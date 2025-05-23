using Microsoft.EntityFrameworkCore;
using UsersManagementService.DAL.Configurations;
using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.DAL.Context;

public class UsersDbContext : DbContext
{
    public UsersDbContext() { }
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Test")
        {
            Database.Migrate();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ImageConfiguration());
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
}