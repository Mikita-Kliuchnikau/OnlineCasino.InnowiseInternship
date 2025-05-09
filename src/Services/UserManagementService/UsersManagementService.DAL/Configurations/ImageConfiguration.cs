using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersManagementService.DAL.Entites.Core;

namespace UsersManagementService.DAL.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
{
    public void Configure(EntityTypeBuilder<ImageEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).IsRequired();
        builder.Property(i => i.UserId).IsRequired();

        builder.HasQueryFilter(image => !image.IsDeleted);
    }
}