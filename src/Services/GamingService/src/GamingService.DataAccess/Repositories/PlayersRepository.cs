using GamingService.Core.Abstractions;
using GamingService.Core.Contracts;
using Grpc.Core;
using UsersManagementService.Grpc;

namespace GamingService.DataAccess.Repositories;

public class PlayersRepository(UsersService.UsersServiceClient usersServiceClient) : IPlayersRepository
{
    public async Task<PlayersRepositoryResponse> DeductedFormPlayersBalanceAsync(
        Guid userId, 
        decimal betAmount, 
        CancellationToken cancellationToken = default)
    {
        var request = new DeductedFromUserBalanceRequest
        {
            MessageId = Guid.NewGuid().ToString(),
            UserId = userId.ToString(),
            Amount = (double)betAmount
        };

        try
        {
            await usersServiceClient.DeductedFromUserBalanceAsync(request, cancellationToken: cancellationToken);
            return new(true, string.Empty);
        }
        catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.AlreadyExists)
        {
            return new(true, string.Empty);
        }
        catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.Unknown)
        {
            return new(false, string.Empty);
        }
        catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.Unauthenticated)
        {
            return new(false, string.Empty);
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }

    public async Task<PlayersRepositoryResponse> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var request = new UserExistsRequest 
        { 
            MessageId = Guid.NewGuid().ToString(),
            UserId = id.ToString() 
        };

        try
        {
            await usersServiceClient.UserExistsAsync(request, cancellationToken: cancellationToken);
            return new(true, string.Empty);
        }
        catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.AlreadyExists)
        {
            return new(true, string.Empty);
        }
        catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.NotFound)
        {
            return new(false, string.Empty);
        }
        catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.Unauthenticated)
        {
            return new(false, string.Empty);
        }
        catch (Exception ex)
        {
            return new(false, ex.Message);
        }
    }
}