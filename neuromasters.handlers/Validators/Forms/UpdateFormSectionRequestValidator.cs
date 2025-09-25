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

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome da seção é obrigatório.")
            .MaximumLength(250).WithMessage("O nome da seção não pode exceder 250 caracteres.");

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0).WithMessage("A ordem da seção deve ser um número não negativo.");

        RuleFor(x => x.Questions)
            .NotEmpty().WithMessage("Cada seção deve conter pelo menos uma pergunta.")
            .ForEach(question => question.SetValidator(new UpdateFormQuestionRequestValidator())); // Validador para cada pergunta
    }
}
