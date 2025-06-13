using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static UsersManagementService.DAL.Constants.BlobStorageConstants;

namespace UsersManagementService.DAL.Options;

public class BlobStorageOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<BlobStorageOptions>
{
    public void Configure(BlobStorageOptions options)
    {
        var connectionString = configuration.GetConnectionString(ImagesBlobStorageConnectionStringName);
        options.ConnectionString = connectionString!;
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}