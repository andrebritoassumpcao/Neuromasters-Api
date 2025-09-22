using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.Validators
{
    public class GetSkillGroupRequestValidator : AbstractValidator<GetSkillGroupRequest>
    {
        public GetSkillGroupRequestValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code é obrigatório");
        }
    }
}
