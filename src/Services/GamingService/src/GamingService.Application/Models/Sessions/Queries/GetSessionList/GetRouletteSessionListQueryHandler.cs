using AutoMapper;
using GamingService.Core.Abstractions;
using GamingService.Core.Contracts;
using MediatR;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionList;

public class GetRouletteSessionListQueryHandler(ISessionsRepository repository, IMapper mapper) 
    : IRequestHandler<GetRouletteSessionListQuery, RouletteSessionListViewModel>
{
    public async Task<RouletteSessionListViewModel> Handle(GetRouletteSessionListQuery request, CancellationToken cancellationToken)
    {
        var filter = mapper.Map<PagedRouletteSessionsFilter>(request);
        var pagedSessions = await repository.GetPagedAsync(filter, cancellationToken);
        return mapper.Map<RouletteSessionListViewModel>(pagedSessions);
    }
}
