using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using UsersManagementService.BLL.Interfaces.Services;
using UsersManagementService.Common.Exceptions;
using UsersManagementService.Grpc;
using static UsersManagementService.Presentation.Constants.GrpcExceptionsMessages;

namespace UsersManagementService.Presentation.gRPC.Services;

public class UsersGrpcService(IUsersService usersService) : UsersService.UsersServiceBase
{
    public override async Task<Empty> UserExists(UserExistsRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.UserId, out var userId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, InvalidUserIdFormat));
        }

        try
        {
            var userExists = await usersService.ExistsUserAsync(userId, context.CancellationToken);
            context.Status = userExists
                ? new Status(StatusCode.OK, "")
                : new Status(StatusCode.NotFound, string.Format(UserNotFound, userId));
        }
        catch (ValidationException)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, InvalidUserIdFormat));
        }

        return new Empty();
    }
    
    public override async Task<Empty> DeductedFromUserBalance(DeductedFromUserBalanceRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.UserId, out var userId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, InvalidUserIdFormat));
        }

        try
        {
            var amount = Convert.ToDecimal(request.Amount) * -1m;
            var balaceChanged = await usersService.TryChangeUserBalanceAsync(userId, amount, context.CancellationToken);
            context.Status = balaceChanged
                ? new Status(StatusCode.OK, "")
                : new Status(StatusCode.Unknown, UserBalanceDeductionFailed);
        }
        catch (ValidationException)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, InvalidUserIdFormat));
        }
        catch (NotFoundException)
        {
            throw new RpcException(new Status(StatusCode.NotFound, string.Format(UserNotFound, userId)));
        }

        return new Empty();
    }
}
