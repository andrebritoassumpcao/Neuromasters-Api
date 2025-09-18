using FluentValidation;
using neuromasters.borders.Dtos.Roles;

namespace neuromasters.handlers.Validators
{
    public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleRequestValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty()
                .WithMessage("Nome da role é obrigatório")
                .MinimumLength(2)
                .WithMessage("Nome da role deve ter pelo menos 2 caracteres")
                .MaximumLength(50)
                .WithMessage("Nome da role deve ter no máximo 50 caracteres")
                .Matches("^[a-zA-Z0-9_-]+$")
                .WithMessage("Nome da role deve conter apenas letras, números, _ ou -");
        }
    }
}
