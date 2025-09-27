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
    }
}
