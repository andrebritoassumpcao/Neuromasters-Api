using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.Validators.Forms
{
    public class ListDefaultAnswerRequestValidator : AbstractValidator<ListDefaultAnswerRequest>
    {
        public ListDefaultAnswerRequestValidator()
        {
            RuleFor(x => x.QuestionnaireId)
                .GreaterThan(0)
                .WithMessage("QuestionnaireId deve ser maior que zero.");
        }
    }
}
