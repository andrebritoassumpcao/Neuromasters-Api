using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.Validators.Forms;

public class CreateFormSectionRequestValidator : AbstractValidator<CreateFormSectionRequest>
{
    public CreateFormSectionRequestValidator()
    {
        // Validação da Seção
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome da seção é obrigatório")
            .MaximumLength(200)
            .WithMessage("Nome da seção deve ter no máximo 200 caracteres");

        RuleFor(x => x.Order)
            .GreaterThan(0)
            .WithMessage("Ordem da seção deve ser maior que zero");

        // Validação das Perguntas
        RuleFor(x => x.Questions)
            .NotEmpty()
            .WithMessage("Seção deve ter pelo menos uma pergunta");

        RuleForEach(x => x.Questions)
            .SetValidator(new CreateFormQuestionRequestValidator());

        // Validação de ordem das perguntas (não pode ter duplicatas)
        RuleFor(x => x.Questions)
            .Must(questions => questions.Select(q => q.Order).Distinct().Count() == questions.Count())
            .WithMessage("Perguntas não podem ter a mesma ordem")
            .When(x => x.Questions != null && x.Questions.Any());
    }
}
