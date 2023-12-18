using DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic;
public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories(configuration);
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IWalletService, WalletService>();
    }
}