using neuromasters.borders.Adapters;
using neuromasters.borders.Adapters.Interfaces;

namespace neuromasters.api.Configurations;

public static class AdaptersConfig
{
    public static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        services.AddScoped<IUserAdapter, UserAdapter>();

        return services;
    }
}
