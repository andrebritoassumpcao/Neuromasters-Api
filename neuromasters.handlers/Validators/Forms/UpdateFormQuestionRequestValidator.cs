using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.Validators.Forms;

public class UpdateFormQuestionRequestValidator : AbstractValidator<UpdateFormQuestionRequest>
{
    public UpdateFormQuestionRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O ID da pergunta é obrigatório.")
            .GreaterThan(0).WithMessage("O ID da pergunta deve ser um número positivo.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("O texto da pergunta é obrigatório.")
            .MaximumLength(1000).WithMessage("O texto da pergunta não pode exceder 1000 caracteres.");

        RuleFor(x => x.Observations)
            .MaximumLength(1000).WithMessage("As observações da pergunta não podem exceder 1000 caracteres.");

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0).WithMessage("A ordem da pergunta deve ser um número não negativo.");
    }
}
