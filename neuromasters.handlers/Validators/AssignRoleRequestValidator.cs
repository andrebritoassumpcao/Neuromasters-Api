using FluentValidation;
using neuromasters.borders.Dtos.Roles;

namespace neuromasters.handlers.Validators;

public class AssignRoleRequestValidator : AbstractValidator<AssignRoleRequest>
{
    public AssignRoleRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId é obrigatório");

        RuleFor(x => x.RoleName)
            .NotEmpty()
            .WithMessage("Nome da role é obrigatório")
            .MinimumLength(2)
            .WithMessage("Nome da role deve ter pelo menos 2 caracteres");
    }
}
