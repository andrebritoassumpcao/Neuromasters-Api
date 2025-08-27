using neuromasters.borders.Shared;
namespace neuromasters.api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static ApplicationConfig LoadConfiguration(this IConfiguration configuration)
        {
            var appConfig = configuration.Get<ApplicationConfig>();
            return appConfig!;
        }
    }
}
