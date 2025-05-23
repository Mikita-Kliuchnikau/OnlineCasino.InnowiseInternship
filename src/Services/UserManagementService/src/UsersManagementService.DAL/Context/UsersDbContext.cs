using Microsoft.EntityFrameworkCore;
using UsersManagementService.DAL.Configurations;
using UsersManagementService.DAL.Entites.Core;
using static UsersManagementService.Common.Constants.Environments;

namespace UsersManagementService.DAL.Context;

public class UsersDbContext : DbContext
{
    public UsersDbContext() { }
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
        if (Environment.GetEnvironmentVariable(AspNetCoreEnvironment) != TestEnvironment)
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