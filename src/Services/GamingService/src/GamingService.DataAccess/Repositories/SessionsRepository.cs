using AutoMapper;
using GamingService.Core.Abstractions;
using GamingService.Core.Contracts;
using GamingService.Core.Models.SessionAggregate;
using GamingService.DataAccess.Entities;
using GamingService.DataAccess.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Data;

namespace GamingService.DataAccess.Repositories;

public class SessionsRepository(
    IOptions<DatabaseOptions> options,
    IMongoClient mongoClient,
    IMapper mapper) : ISessionsRepository
{
    private IMongoDatabase Database => mongoClient.GetDatabase(options.Value.DatabaseName);
    private IMongoCollection<RouletteSessionDocument> Collection => Database.GetCollection<RouletteSessionDocument>(options.Value.SessionsCollection);

    public async Task<RouletteSession> CreateAsync(RouletteSession session, CancellationToken cancellationToken = default)
    {
        var document = mapper.Map<RouletteSessionDocument>(session);

        await Collection.InsertOneAsync(document, cancellationToken: cancellationToken);

        return mapper.Map<RouletteSession>(document);
    }

    public async Task<RouletteSession> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var document = await Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

        return mapper.Map<RouletteSession>(document);
    }

    public async Task<PagedRouletteSessionsProjection> GetPagedAsync(PagedRouletteSessionsFilter filter, CancellationToken cancellationToken = default)
    {
        var skip = (filter.PageNumber - 1) * filter.PageSize;

        var conuntFacetName = "totalCount";
        var dataFacetName = "data";

        var countFacet = AggregateFacet.Create(conuntFacetName,
            PipelineDefinition<RouletteSessionDocument, AggregateCountResult>.Create(
            [
                PipelineStageDefinitionBuilder.Count<RouletteSessionDocument>()
            ]));

        var dataFacet = AggregateFacet.Create(dataFacetName,
            PipelineDefinition<RouletteSessionDocument, RouletteSessionDocument>.Create(
            [
                PipelineStageDefinitionBuilder.Skip<RouletteSessionDocument>(skip),
                PipelineStageDefinitionBuilder.Limit<RouletteSessionDocument>(filter.PageSize)
            ]));

        var aggregation = await Collection
            .Aggregate()
            .Facet(countFacet, dataFacet)
            .ToListAsync(cancellationToken);
            

        var count = aggregation[0]
            .Facets.First(x => x.Name == conuntFacetName)
            .Output<AggregateCountResult>()[0]
            .Count;

        var sessions = aggregation[0]
            .Facets.First(x => x.Name == dataFacetName)
            .Output<RouletteSessionDocument>();

        var mappedSessions = sessions
            .Select(x => mapper.Map<RouletteSession>(x))
            .ToList();

        return new PagedRouletteSessionsProjection(
            Sessions: mappedSessions,
            TotalCount: (int)count,
            PageNumber: filter.PageNumber,
            PageSize: filter.PageSize);
    }

    public async Task<RouletteSession> CloseAsync(RouletteSession rouletteSession, CancellationToken cancellationToken = default)
    {
        var document = mapper.Map<RouletteSessionDocument>(rouletteSession);

        using var session = await mongoClient.StartSessionAsync(cancellationToken: cancellationToken);
        
        session.StartTransaction();

        RouletteSessionDocument? result = new(); 
        try
        {
            result = await Collection.FindOneAndUpdateAsync(x => x.Id == document.Id,
                Builders<RouletteSessionDocument>.Update
                    .Set(x => x.Status, document.Status)
                    .Set(x => x.Bets, document.Bets),
                cancellationToken: cancellationToken);
        }
        catch (Exception)
        {
            await session.AbortTransactionAsync(cancellationToken: cancellationToken);
        }
        finally
        {
            await session.CommitTransactionAsync(cancellationToken);
        }

        return mapper.Map<RouletteSession>(result);
    }
}
