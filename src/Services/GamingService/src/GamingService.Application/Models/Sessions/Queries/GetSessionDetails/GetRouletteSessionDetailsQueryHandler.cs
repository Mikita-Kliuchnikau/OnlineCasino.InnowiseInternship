using AutoMapper;
using GamingService.Core.Abstractions;
using MediatR;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public class GetRouletteSessionDetailsQueryHandler(ISessionsRepository repository, IMapper mapper) 
    : IRequestHandler<GetRouletteSessionDetailsQuery, RouletteSessionViewModel>
{
    public async Task<RouletteSessionViewModel> Handle(GetRouletteSessionDetailsQuery request, CancellationToken cancellationToken)
    {
        var session = await repository.GetByIdAsync(Guid.Parse(request.SessionId), cancellationToken)
            ?? throw new ArgumentException($"Session with id {request.SessionId} not found", nameof(request));
        return mapper.Map<RouletteSessionViewModel>(session);
    }
}
