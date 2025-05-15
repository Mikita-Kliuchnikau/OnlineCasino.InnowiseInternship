using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UsersManagementService.DAL.Entites.Core;
namespace UsersManagementService.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(user => user.Id);

        builder.HasMany(user => user.Images)
            .WithOne(image => image.User)
            .HasForeignKey(image => image.UserId); 

        builder.Property(user => user.Id).IsRequired();
        builder.Property(user => user.AuthId).IsRequired();
        builder.Property(user => user.Username).HasMaxLength(50).IsRequired();
        builder.Property(user => user.Email).HasMaxLength(100).IsRequired();
        builder.Property(user => user.Balance).IsRequired();
        builder.Property(user => user.VerificationStatus).IsRequired();
        builder.Property(user => user.IsBanned).IsRequired();
        builder.Property(user => user.FirstName).HasMaxLength(50);
        builder.Property(user => user.SecondName).HasMaxLength(50);
        builder.Property(user => user.LastName).HasMaxLength(50);
        builder.Property(user => user.BirthDate);
        builder.Property(user => user.PassportNumber).HasMaxLength(50);
        builder.Property(user => user.IdentificationNumber).HasMaxLength(50);

        builder.HasQueryFilter(user => !user.IsDeleted);
    }
}