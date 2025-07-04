using AutoMapper;
using GamingService.Core.Abstractions;
using MediatR;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public class GetRouletteSessionDetailsQueryHandler(ISessionsRepository repository, IMapper mapper) 
    : IRequestHandler<GetRouletteSessionDetailsQuery, RouletteSessionVm>
{
    public async Task<RouletteSessionVm> Handle(GetRouletteSessionDetailsQuery request, CancellationToken cancellationToken)
    {
        var session = await repository.GetByIdAsync(request.SessionId, cancellationToken)
            ?? throw new ArgumentException($"Session with id {request.SessionId} not found", nameof(request));
        return mapper.Map<RouletteSessionVm>(session);
    }
}
