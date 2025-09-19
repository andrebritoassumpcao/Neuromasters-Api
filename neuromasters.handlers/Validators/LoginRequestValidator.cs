using FluentValidation;
using neuromasters.borders.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório")
                .EmailAddress()
                .WithMessage("Email deve ter um formato válido");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Senha é obrigatória");
        }
    }
}
