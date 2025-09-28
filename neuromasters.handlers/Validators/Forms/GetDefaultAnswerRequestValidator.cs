using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;

namespace neuromasters.handlers.Validators.Forms;

public class GetDefaultAnswerRequestValidator : AbstractValidator<GetDefaultAnswerRequest>
{
    public GetDefaultAnswerRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id deve ser maior que zero.");
    }
}
