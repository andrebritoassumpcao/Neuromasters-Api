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

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do questionário é obrigatório.")
                .MaximumLength(250).WithMessage("O nome do questionário não pode exceder 250 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("A descrição do questionário não pode exceder 1000 caracteres.");


            // 🔹 Validação das Seções (Sections)
            RuleFor(x => x.Sections)
                .NotEmpty().WithMessage("O questionário deve conter pelo menos uma seção.")
                .ForEach(section => section.SetValidator(new UpdateFormSectionRequestValidator()));
        }
    }
}
