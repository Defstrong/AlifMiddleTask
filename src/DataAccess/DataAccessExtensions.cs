using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DataAccess;

public static class DataAccessExtensions
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AlifDbContext>(options => options.UseNpgsql(configuration.GetEnvConnectionString()));
    }
}