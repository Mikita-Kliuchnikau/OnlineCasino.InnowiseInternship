using GamingService.Contracts.Events;
using MassTransit;
using UsersManagementService.BLL.Interfaces.Services;

namespace UsersManagementService.Presentation.EventConsumers;

public sealed class UserBalanceChangedConsumer(IUsersService service) : IConsumer<PlayersBalancesChangedEvent>
{
    public async Task Consume(ConsumeContext<PlayersBalancesChangedEvent> context)
    {
        await service.TryChangeUserBalanceAsync(
            context.Message.PlayerId,
            context.Message.Amount);
    }
}
