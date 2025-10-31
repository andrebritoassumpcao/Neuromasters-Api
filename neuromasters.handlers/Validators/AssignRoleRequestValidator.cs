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

        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Id da role é obrigatório");
    }
}
