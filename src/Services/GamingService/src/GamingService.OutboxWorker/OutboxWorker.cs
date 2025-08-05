using GamingService.Contracts.Documents;
using GamingService.Contracts.Enums;
using GamingService.Contracts.Events;
using GamingService.OutboxWorker.Abstractions;
using GamingService.OutboxWorker.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using EventDocument = GamingService.Contracts.Documents.OutboxMessageDocument<GamingService.Contracts.Events.PlayersBalancesChangedEvent>;

namespace GamingService.OutboxWorker
{
    public class OutboxWorker(
        IMongoClient mongoClient,
        IServiceProvider serviceProvider,
        IOptions<DatabaseOptions> options,
        ILogger<OutboxWorker> logger) : BackgroundService
    {
        private IMongoDatabase Database => mongoClient.GetDatabase(options.Value.DatabaseName);
        private IMongoCollection<EventDocument> Collection =>
            Database.GetCollection<EventDocument>(options.Value.OutboxCollectionName);

        private List<Guid?> ÑancelledEventIds { get; } = [];

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            var publisher = scope.ServiceProvider.GetRequiredService<IIntegrationEventPublisher>();


            var eventDocuments = await Collection
                .Find(d => d.Status == Status.Pending)
                .Limit(20)
                .ToListAsync(stoppingToken);

            var idsToUpdate = eventDocuments.Select(d => d.MessageId).ToList();
            if (idsToUpdate.Count > 0)
            {
                var update = Builders<EventDocument>.Update.Set(x => x.Status, Status.Processing);
                await Collection.UpdateManyAsync(
                    Builders<EventDocument>.Filter.In(x => x.MessageId, idsToUpdate),
                    update,
                    cancellationToken: stoppingToken);
            }

            foreach (var doc in eventDocuments)
            {
                try
                {
                    var @event = doc.Payload;
                    await publisher.PublishAsync(@event, stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to process outbox message {Id}", doc.MessageId);
                }
            }

            if (ÑancelledEventIds.Count > 0)
            {
                var update = Builders<EventDocument>.Update.Set(x => x.Status, Status.Pending);
                await Collection.UpdateManyAsync(
                    Builders<EventDocument>.Filter.In(x => x.MessageId, ÑancelledEventIds),
                    update,
                    cancellationToken: stoppingToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
        }
    }
}
