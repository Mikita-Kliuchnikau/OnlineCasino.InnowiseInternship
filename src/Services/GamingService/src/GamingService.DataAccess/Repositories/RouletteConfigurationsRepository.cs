using AutoMapper;
using GamingService.Core.Abstractions;
using GamingService.Core.Contracts;
using GamingService.Core.Models.RouletteConfigurationAggregate;
using GamingService.DataAccess.Entities;
using GamingService.DataAccess.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GamingService.DataAccess.Repositories;

public class RouletteConfigurationsRepository(
    IOptions<DatabaseOptions> options,
        IMongoClient mongoClient,
        IMapper mapper) : IRouletteConfiguratonsRepository
{
    private IMongoDatabase Database => mongoClient.GetDatabase(options.Value.DatabaseName);
    private IMongoCollection<RouletteConfigurationDocument> Collection => 
        Database.GetCollection<RouletteConfigurationDocument>(options.Value.GamesCollection);

    public async Task<RouletteConfiguration> CreateAsync(RouletteConfiguration configuration, CancellationToken cancellationToken = default)
    {
        var document = mapper.Map<RouletteConfigurationDocument>(configuration);

        await Collection.InsertOneAsync(document, cancellationToken: cancellationToken);

        return mapper.Map<RouletteConfiguration>(document);
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteOneAsync(x => x.Id == id, cancellationToken);

        return id;
    }

    public async Task<RouletteConfiguration> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var document = await Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

        return mapper.Map<RouletteConfiguration>(document);
    }

    public async Task<PagedRouletteConfigurationsProjection> GetPagedAsync(
        PagedRouletteConfigurationsFilter filter,
        CancellationToken cancellationToken = default)
    {
        var skip = (filter.PageNumber - 1) * filter.PageSize;

        var conuntFacetName = "totalCount";
        var dataFacetName = "data";

        var countFacet = AggregateFacet.Create(conuntFacetName,
            PipelineDefinition<RouletteConfigurationDocument, AggregateCountResult>.Create(
            [
                PipelineStageDefinitionBuilder.Count<RouletteConfigurationDocument>()
            ]));

        var dataFacet = AggregateFacet.Create(dataFacetName,
            PipelineDefinition<RouletteConfigurationDocument, RouletteConfigurationDocument>.Create(
            [
                PipelineStageDefinitionBuilder.Skip<RouletteConfigurationDocument>(skip),
                PipelineStageDefinitionBuilder.Limit<RouletteConfigurationDocument>(filter.PageSize)
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
            .Output<RouletteConfigurationDocument>();

        var mappedConfiguration = sessions
            .Select(x => mapper.Map<RouletteConfiguration>(x))
            .ToList();

        return new PagedRouletteConfigurationsProjection(
            Configurations: mappedConfiguration,
            TotalCount: (int)count,
            PageNumber: filter.PageNumber,
            PageSize: filter.PageSize);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var document = await Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        return document != null;
    }
}
