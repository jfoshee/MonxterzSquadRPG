using Monxterz.SquadRpg.GameClient;

// .NET Practice is to place ServiceCollectionExtensions in the following namespace
// to improve discoverability of the extension method during service configuration
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMonxterzSquad(this IServiceCollection services)
    {
        return services.AddGameStateClientServices(Constants.GameMasterId, "https://localhost:7264");
    }
}
