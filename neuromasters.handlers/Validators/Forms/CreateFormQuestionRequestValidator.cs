using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.Validators.Forms
{
    public class CreateFormQuestionRequestValidator : AbstractValidator<CreateFormQuestionRequest>
    {
        public CreateFormQuestionRequestValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Texto da pergunta é obrigatório")
                .MaximumLength(500)
                .WithMessage("Texto da pergunta deve ter no máximo 500 caracteres");

            RuleFor(x => x.Observations)
                .MaximumLength(1000)
                .WithMessage("Observações devem ter no máximo 1000 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Observations));

            RuleFor(x => x.Order)
                .GreaterThan(0)
                .WithMessage("Ordem da pergunta deve ser maior que zero");
        }
    }
}
