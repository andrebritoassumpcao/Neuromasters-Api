using neuromasters.borders.UseCases.Auth;
using neuromasters.borders.UseCases.Roles;
using neuromasters.handlers.UseCases.Auth;
using neuromasters.handlers.UseCases.Roles;

namespace neuromasters.api.Configurations;
public static class UseCasesConfig
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ICreateRoleUseCase, CreateRoleUseCase>();
        services.AddScoped<IAssignRoleUseCase, AssignRoleUseCase>();
        services.AddScoped<IGetUserRoleUseCase, GetUserRoleUseCase>();
        services.AddScoped<IListRolesUseCase, ListRolesUseCase>();

        return services;
    }
}
