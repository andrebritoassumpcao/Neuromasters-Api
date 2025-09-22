using neuromasters.borders.Repositories;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Auth;
using neuromasters.handlers.UseCases.Auth;
using neuromasters.repositories;
using neuromasters.repositories.Questionnaires;
using System.Data;
using System.Net.Mime;

namespace neuromasters.api.Configurations
{
    public static class RepositoriesConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, ApplicationConfig appConfig)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<ISkillGroupRepository, SkillGroupRepository>();

            return services;
        }
    }
}
