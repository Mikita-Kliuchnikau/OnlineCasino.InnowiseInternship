using Microsoft.EntityFrameworkCore;
using System.Linq;
using UsersManagementService.DAL.Context;
using UsersManagementService.DAL.Entites;
using UsersManagementService.DAL.Interfaces;

namespace UsersManagementService.DAL.Repositories;

public class UsersRepository(UsersDbContext context) : IUsersRepository
{
    public async Task<Guid> CreateAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await context.Users
                .Where(user => user.Id == id)
                .ExecuteDeleteAsync();

        return id;
    }

    public async Task<List<UserEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Users
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<UserEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == id);

        return user;
    }

    public async Task<PagedUsersProjection> GetPagedAsync(
        PagedUsersFilter pagedUsersRequest, 
        CancellationToken cancellationToken = default)
    {
        var users = await context.Users
            .AsNoTracking()
            .Skip((pagedUsersRequest.PageNumber - 1) * pagedUsersRequest.PageSize)
            .Take(pagedUsersRequest.PageSize)
            .ToListAsync();

        return new PagedUsersProjection
        {
            PageNumber = pagedUsersRequest.PageNumber,
            TotalCount = context.Users.AsNoTracking().Count(),
            Projection = users
        };
    }

    public async Task<Guid> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        var entity = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.Id == user.Id);

        context.Users
            .Entry(entity)
            .CurrentValues
            .SetValues(user);
                
        await context.SaveChangesAsync();

        return user.Id;
    }
}
