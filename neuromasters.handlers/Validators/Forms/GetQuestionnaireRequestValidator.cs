using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.Forms;

namespace neuromasters.handlers.Validators.Forms;

public class GetQuestionnaireRequestValidator : AbstractValidator<GetQuestionnaireRequest>
{
    public GetQuestionnaireRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("ID do questionário deve ser maior que zero");
    }
}
