using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace UsersManagementService.DAL.Options;

public class DatabaseOptionsSetup(IConfiguration configuration) 
    : IConfigureOptions<DatabaseOptions>
{
    private const string ConfigurationSectionName = "DatabaseOptions";

    public void Configure(DatabaseOptions options)
    {
        var connectionString = configuration.GetConnectionString("UsersDatabase");
         
        options.ConnectionString = connectionString;    

        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
