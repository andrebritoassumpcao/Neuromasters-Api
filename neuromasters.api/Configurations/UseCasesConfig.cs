using neuromasters.borders.UseCases.Auth;
using neuromasters.handlers.UseCases.Auth;

namespace neuromasters.api.Configurations;
public static class UseCasesConfig
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        return services;
    }
}
