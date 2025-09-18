using FluentValidation;
using neuromasters.borders.Dtos.Auth;
using neuromasters.borders.Dtos.Roles;
using neuromasters.handlers.Validators;

namespace neuromasters.api.Configurations;

public static class ValidatorsConfig
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
        services.AddScoped<IValidator<CreateRoleRequest>, CreateRoleRequestValidator>();
        services.AddScoped<IValidator<AssignRoleRequest>, AssignRoleRequestValidator>();

        return services;
    }

}
