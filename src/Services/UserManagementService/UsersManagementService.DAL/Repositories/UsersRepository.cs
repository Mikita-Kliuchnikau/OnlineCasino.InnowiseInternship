using Microsoft.EntityFrameworkCore;
using System.Linq;
using UsersManagementService.DAL.Entites;
using UsersManagementService.DAL.Interfaces;

namespace UsersManagementService.DAL.Repositories;

public class UsersRepository(IUsersDbContext context) : IUsersRepository
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
        var usersEntities = await context.Users
                .AsNoTracking()
                .ToListAsync();

        var users = usersEntities
            .Select(user => user)
            .ToList();

        return users;
    }

    public async Task<UserEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == id);

        return user;
    }

    public async Task<PagedUsersResponse> GetPageAsync(
        PagedUsersRequest pagedUsersRequest, 
        CancellationToken cancellationToken = default)
    {
        var users = await context.Users
            .AsNoTracking()
            .Skip((pagedUsersRequest.PageNumber - 1) * pagedUsersRequest.PageSize)
            .Take(pagedUsersRequest.PageSize)
            .ToListAsync();

        return new PagedUsersResponse
        {
            PageNumber = pagedUsersRequest.PageNumber,
            TotalCount = context.Users.AsNoTracking().Count(),
            Responce = users
        };
    }

    public async Task<Guid> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        await context.Users
                .Where(userEntity => userEntity.Id == user.Id)
                .ExecuteUpdateAsync(userEntity => userEntity
                .SetProperty(u => u.UserName, u => user.UserName)
                .SetProperty(u => u.Email, u => user.Email)
                .SetProperty(u => u.VerificationStatus, u => user.VerificationStatus)
                .SetProperty(u => u.IsBanned, u => user.IsBanned)
                .SetProperty(u => u.FirstName, u => user.FirstName)
                .SetProperty(u => u.SecondName, u => user.SecondName)
                .SetProperty(u => u.LastName, u => user.LastName)
                .SetProperty(u => u.BirthDate, u => user.BirthDate)
                .SetProperty(u => u.PassportNumber, u => user.PassportNumber)
                .SetProperty(u => u.IdentificationNumber, u => user.IdentificationNumber));

        return user.Id;
    }
}
