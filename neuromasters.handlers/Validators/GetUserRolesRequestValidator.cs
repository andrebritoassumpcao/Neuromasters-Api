using FluentValidation;
using neuromasters.borders.Dtos.Roles;

namespace neuromasters.handlers.Validators;

public class GetUserRolesRequestValidator : AbstractValidator<GetUserRolesRequest>
{
    public GetUserRolesRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId é obrigatório");
    }
}
