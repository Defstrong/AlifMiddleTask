using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DataAccess;

public static class RepositoryExtension
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AlifDbContext>(options 
            => options.UseLazyLoadingProxies().UseNpgsql(configuration.GetEnvConnectionString()));
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
    }
}