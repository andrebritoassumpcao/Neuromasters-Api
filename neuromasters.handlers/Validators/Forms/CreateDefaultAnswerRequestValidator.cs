using FluentValidation;
using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;

namespace neuromasters.handlers.Validators.Forms
{
    public class CreateDefaultAnswerRequestValidator : AbstractValidator<CreateDefaultAnswerRequest>
    {
        public CreateDefaultAnswerRequestValidator()
        {
            RuleFor(x => x.QuestionnaireId)
                .GreaterThan(0)
                .WithMessage("QuestionnaireId deve ser maior que zero.");

            RuleFor(x => x.Label)
                .NotEmpty()
                .WithMessage("Label é obrigatório.")
                .MaximumLength(255)
                .WithMessage("Label deve ter no máximo 255 caracteres.");

            RuleFor(x => x.Color)
                .NotEmpty()
                .WithMessage("Color é obrigatório.");

        }
    }
}
