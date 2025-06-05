using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static UsersManagementService.DAL.Constants.DatabaseConstants;

namespace UsersManagementService.DAL.Options;

public class DatabaseOptionsSetup(IConfiguration configuration) 
    : IConfigureOptions<DatabaseOptions>
{
    public void Configure(DatabaseOptions options)
    {
        var connectionString = configuration
            .GetConnectionString(UsersDatabaseConnectionStringName);
         
        options.ConnectionString = connectionString!;    

        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}