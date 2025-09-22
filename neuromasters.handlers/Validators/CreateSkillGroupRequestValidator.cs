using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.Validators
{
    public class CreateSkillGroupRequestValidator : AbstractValidator<CreateSkillGroupRequest>
    {
        public CreateSkillGroupRequestValidator() 
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Nome do grupo não pode estar vazio")
                .MinimumLength(2)
                .WithMessage("Nome do grupo deve ter pelo menos 2 caracteres");
        }
    }
}
