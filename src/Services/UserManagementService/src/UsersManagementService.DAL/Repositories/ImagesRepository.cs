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
        try
        {
            await context.Images.AddAsync(image, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new NotFoundException(nameof(image.User), image.UserId);
        }

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

    public async Task<bool> DoesImageExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Images.AnyAsync(u => u.Id == id, cancellationToken);
    }
}