using GamingService.Core.Common;
using GamingService.Core.Primitives;

namespace GamingService.Core.Contracts;

public record PlayerBalanceChange(Id PlayerId, Currency Currency, decimal Amount);
