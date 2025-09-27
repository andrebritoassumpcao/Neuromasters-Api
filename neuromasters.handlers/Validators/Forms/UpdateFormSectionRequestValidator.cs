using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.Validators.Forms;

public class UpdateFormSectionRequestValidator : AbstractValidator<UpdateFormSectionRequest>
{
    public UpdateFormSectionRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O ID da seção é obrigatório.")
            .GreaterThan(0).WithMessage("O ID da seção deve ser um número positivo.");

    }
}
