using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.Form;

namespace neuromasters.handlers.UseCases.Questionnaires.Forms;

public class CreateQuestionnaireUseCase(
      ILogger<CreateQuestionnaireUseCase> logger,
      IValidator<CreateQuestionnaireRequest> validator,
      IQuestionnaireRepository repository)
      : UseCase<CreateQuestionnaireRequest, QuestionnaireDetailDto>(logger, validator),
        ICreateQuestionnaireUseCase
{
    protected override async Task<UseCaseResponse<QuestionnaireDetailDto>> OnExecute(CreateQuestionnaireRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var created = await repository.AddAsync(request);

        return Persisted(created, created.Id.ToString());
    }
}
