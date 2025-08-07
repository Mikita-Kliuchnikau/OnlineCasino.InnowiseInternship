using GamingService.OutboxWorker;
using GamingService.OutboxWorker.Abstractions;
using GamingService.OutboxWorker.Events;
using GamingService.OutboxWorker.Extensions;
using GamingService.OutboxWorker.Options;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using static GamingService.OutboxWorker.Extensions.DIExtensions;

var builder = Host.CreateApplicationBuilder(args);

builder.UseSerilog(builder.Configuration);

builder.Services.AddHostedService<OutboxWorker>();
builder.Services.ConfigureOptions<RabbitMqOptionsSetup>();
builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.AddScoped<IIntegrationEventPublisher, RabbitMqPlayersBalancesChangedEventPublisher>();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
        configurator.Host(options.Host, h =>
        {
            h.Username(options.Username);
            h.Password(options.Password);
        });
        configurator.ConfigureEndpoints(context);
    });
});

builder.Services.AddSingleton<IMongoClient>(provider =>
{
    var options = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
    var mongoOptions = new MongoClientSettings
    {
        Server = new MongoServerAddress(options.Host, options.Port),
        Credential = MongoCredential.CreateCredential(options.CredentialSource, options.Username, options.Password),
        ReplicaSetName = options.ReplicaSet
    };
    return new MongoClient(mongoOptions);
});

var host = builder.Build();

try
{
    await host.RunAsync();
}
finally
{
    await Log.CloseAndFlushAsync();
}