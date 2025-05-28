using Microsoft.EntityFrameworkCore;
using UsersManagementService.Common.Exceptions;
using UsersManagementService.DAL.Context;
using UsersManagementService.DAL.Entites.Core;
using UsersManagementService.DAL.Interfaces.Repositories;

namespace UsersManagementService.DAL.Repositories;

public class ImagesRepository(UsersDbContext context) : IImagesRepository
{
    public async Task<Guid> CreateAsync(
        ImageEntity image, 
        CancellationToken cancellationToken = default)
    {
        await context.Images.AddAsync(image, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return image.Id;
    }

    public async Task<Guid> DeleteAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var image = await context.Images.FirstOrDefaultAsync(image => image.Id == id, cancellationToken);

        if (image == null)
        {
            throw new NotFoundException(nameof(image), id);
        }
        
        context.Remove<ImageEntity>(image);

        await context.SaveChangesAsync(cancellationToken);

        return id;
    }

    public async Task<Guid> UpdateAsync(
        ImageEntity image, 
        CancellationToken cancellationToken = default)
    {
        if (await DoesImageExistAsync(image.Id, cancellationToken) is false)
        {
            throw new NotFoundException(nameof(image), image.Id);
        }

        context.Images.Update(image);

        await context.SaveChangesAsync(cancellationToken);

        return image.Id;
    }

    public async Task<bool> DoesImageExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Images.AnyAsync(u => u.Id == id, cancellationToken);
    }
}