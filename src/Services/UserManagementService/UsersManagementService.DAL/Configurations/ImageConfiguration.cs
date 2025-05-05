using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersManagementService.DAL.Entites;

namespace UsersManagementService.DAL.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
{
    public void Configure(EntityTypeBuilder<ImageEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasIndex(user => user.Id)
            .IsUnique();
    }
}