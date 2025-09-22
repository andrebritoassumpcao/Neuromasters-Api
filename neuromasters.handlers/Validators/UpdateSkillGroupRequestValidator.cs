using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;

namespace neuromasters.handlers.Validators;

public class UpdateSkillGroupRequestValidator : AbstractValidator<UpdateSkillGroupRequest>
{
    public UpdateSkillGroupRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Nome do grupo não pode estar vazio")
            .MinimumLength(2)
            .WithMessage("Nome do grupo deve ter pelo menos 2 caracteres");
    }
}