using Microsoft.EntityFrameworkCore;
using UsersManagementService.DAL.Entites;
using UsersManagementService.DAL.Interfaces;

namespace UsersManagementService.DAL.Context;

public class UsersDbContext(DbContextOptions<UsersDbContext> options)
    : DbContext(options), IUsersDbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
}
