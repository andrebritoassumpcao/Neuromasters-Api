using neuromasters.borders.Repositories;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Auth;
using neuromasters.handlers.UseCases.Auth;
using neuromasters.repositories;
using System.Data;
using System.Net.Mime;

namespace neuromasters.api.Configurations
{
    public static class RepositoriesConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, ApplicationConfig appConfig)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}
