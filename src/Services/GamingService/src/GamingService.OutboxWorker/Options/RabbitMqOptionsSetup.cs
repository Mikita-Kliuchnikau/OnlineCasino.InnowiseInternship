using Microsoft.Extensions.Options;
using static GamingService.OutboxWorker.Constants.RabbitMqConstants;

namespace GamingService.OutboxWorker.Options;

public class RabbitMqOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<RabbitMqOptions>
{
    public void Configure(RabbitMqOptions options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}