using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.Validators.Forms
{
    public class UpdateQuestionnaireRequestValidator : AbstractValidator<UpdateQuestionnaireRequest>
    {
        public UpdateQuestionnaireRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do questionário é obrigatório.")
                .GreaterThan(0).WithMessage("O ID do questionário deve ser um número positivo.");

        }
    }
}
