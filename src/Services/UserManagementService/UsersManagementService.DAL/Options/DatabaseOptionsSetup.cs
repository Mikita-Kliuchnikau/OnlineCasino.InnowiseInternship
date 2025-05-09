using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UsersManagementService.DAL.Constants;

namespace UsersManagementService.DAL.Options;

public class DatabaseOptionsSetup(IConfiguration configuration) 
    : IConfigureOptions<DatabaseOptions>
{
    public void Configure(DatabaseOptions options)
    {
        var connectionString = configuration
            .GetConnectionString(DatabaseConstants.UsersDatabaseConnectionStringName);
         
        options.ConnectionString = connectionString;    

        configuration.GetSection(DatabaseConstants.ConfigurationSectionName).Bind(options);
    }
}