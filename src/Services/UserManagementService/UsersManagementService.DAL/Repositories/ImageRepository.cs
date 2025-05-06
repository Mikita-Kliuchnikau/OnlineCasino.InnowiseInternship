using Microsoft.EntityFrameworkCore;
using UsersManagementService.DAL.Context;
using UsersManagementService.DAL.Entites;
using UsersManagementService.DAL.Interfaces;

namespace UsersManagementService.DAL.Repositories;

public class ImageRepository(UsersDbContext context) : IImageRepository
{
    public async Task<Guid> CreateAsync(
        ImageEntity image, 
        CancellationToken cancellationToken)
    {
        await context.Images.AddAsync(image, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return image.Id;
    }

    public async Task<Guid> DeleteAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var image = await context.Images.FirstOrDefaultAsync(image => image.Id == id, cancellationToken);
        context.Remove(image);

        await context.SaveChangesAsync(cancellationToken);

        return id;
    }

    public async Task<Guid> UpdateAsync(
        ImageEntity image, 
        CancellationToken cancellationToken)
    {
        var entity = await context.Images.FirstOrDefaultAsync(e => e.Id == image.Id, cancellationToken);

        entity = image;

        await context.SaveChangesAsync(cancellationToken);

        return image.Id;
    }
}
