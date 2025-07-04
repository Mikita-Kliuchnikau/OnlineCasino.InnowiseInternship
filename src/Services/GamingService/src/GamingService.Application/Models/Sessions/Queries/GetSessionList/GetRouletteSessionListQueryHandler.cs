using AutoMapper;
using GamingService.Core.Abstractions;
using MediatR;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionList;

public class GetRouletteSessionListQueryHandler(ISessionsRepository repository, IMapper mapper) 
    : IRequestHandler<GetRouletteSessionListQuery, RouletteSessionListVm>
{
    public async Task<RouletteSessionListVm> Handle(GetRouletteSessionListQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<RouletteSessionListVm>(await repository.GetPagedAsync(request.Filter, cancellationToken));
    }
}
