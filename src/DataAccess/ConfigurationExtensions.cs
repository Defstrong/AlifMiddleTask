using System.Diagnostics;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace DataAccess;

public static class ConfigurationExtension
{
    private const string ConnectionStringName = "DefaultConnection";
    private const string DatabaseUrlKey = "DATABASE_URL";
    private const string AspNetCoreEnvironmentKey = "ASPNETCORE_ENVIRONMENT";
    private const string DevelopmentEnvironmentName = "Development";

    private static string GetConnectionStringFromEnv()
    {
        string connectionStrTemplate = "Server={0};Port={1};User Id={2};Password={3};Database={4}";

        string? databaseUrl = Environment.GetEnvironmentVariable(DatabaseUrlKey);
        Debug.Assert(databaseUrl is { Length: > 0 });

        Uri databaseUri = new(databaseUrl);

        string dbName = databaseUri.LocalPath.TrimStart('/');
        Debug.Assert(dbName is { Length: > 0 });

        string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);
        Debug.Assert(userInfo is { Length: > 0 });

        return string.Format(CultureInfo.InvariantCulture, connectionStrTemplate, 
            databaseUri.Host, databaseUri.Port, userInfo[0], userInfo[1], dbName);
    }

    public static string GetEnvConnectionString(this IConfiguration configuration)
    {
        Debug.Assert(configuration is not null);

        string currentEnv = Environment.GetEnvironmentVariable(AspNetCoreEnvironmentKey) ?? string.Empty;
        return currentEnv switch
        {
            DevelopmentEnvironmentName => configuration.GetConnectionString(ConnectionStringName) ?? string.Empty,
            _ => GetConnectionStringFromEnv()
        };
    }
}