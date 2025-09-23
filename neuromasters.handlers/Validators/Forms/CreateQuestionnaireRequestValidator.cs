using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Dtos.Questionnaires.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.Validators.Forms;

public class CreateQuestionnaireRequestValidator : AbstractValidator<CreateQuestionnaireRequest>
{
    public CreateQuestionnaireRequestValidator()
    {
        // Validação do Questionário
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome do questionário é obrigatório")
            .MaximumLength(200)
            .WithMessage("Nome do questionário deve ter no máximo 200 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Descrição deve ter no máximo 1000 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Description));

        // Validação das Seções
        RuleFor(x => x.Sections)
            .NotEmpty()
            .WithMessage("Questionário deve ter pelo menos uma seção");

        RuleForEach(x => x.Sections)
            .SetValidator(new CreateFormSectionRequestValidator());

        // Validação de ordem das seções (não pode ter duplicatas)
        RuleFor(x => x.Sections)
            .Must(sections => sections.Select(s => s.Order).Distinct().Count() == sections.Count())
            .WithMessage("Seções não podem ter a mesma ordem")
            .When(x => x.Sections != null && x.Sections.Any());
    }
}