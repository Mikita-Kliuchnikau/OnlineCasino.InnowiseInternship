using GamingService.Core.Contracts;
using MediatR;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionList;

public record GetRouletteSessionListQuery(PagedRouletteSessionsFilter Filter) 
    : IRequest<GetRouletteSessionListViewModel>;
