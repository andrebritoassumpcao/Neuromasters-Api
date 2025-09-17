using FluentValidation;
using neuromasters.borders.Dtos.Auth;
using neuromasters.handlers.Validators;

namespace neuromasters.api.Configurations;

public static class ValidatorsConfig
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();

        return services;
    }

}
