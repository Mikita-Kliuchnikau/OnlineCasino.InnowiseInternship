using AutoMapper;
using GamingService.Core.Abstractions;
using MediatR;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionList;

public class GetRouletteSessionListQueryHandler(ISessionsRepository repository, IMapper mapper) 
    : IRequestHandler<GetRouletteSessionListQuery, GetRouletteSessionListViewModel>
{
    public async Task<GetRouletteSessionListViewModel> Handle(GetRouletteSessionListQuery request, CancellationToken cancellationToken)
    {
        var pagedSessions = await repository.GetPagedAsync(request.Filter, cancellationToken);
        return mapper.Map<GetRouletteSessionListViewModel>(pagedSessions);
    }
}
