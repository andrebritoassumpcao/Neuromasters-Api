using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Dtos.Questionnaires.Forms;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.Form;

namespace neuromasters.handlers.UseCases.Questionnaires.Forms;

public class GetQuestionnaireUseCase(
        ILogger<GetQuestionnaireUseCase> logger,
        IValidator<GetQuestionnaireRequest> validator,
        IQuestionnaireAdapter adapter,
        IQuestionnaireRepository repository)
        : UseCase<GetQuestionnaireRequest, QuestionnaireDetailDto?>(logger, validator),
          IGetQuestionnaireUseCase
{
    protected override async Task<UseCaseResponse<QuestionnaireDetailDto?>> OnExecute(GetQuestionnaireRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var questionnaire = await repository.GetByIdWithDetailsAsync(request.Id);
        if (questionnaire is null)
            return BadRequest(new ErrorMessage("QT.01", "Questionário não encontrado"));

        var result = adapter.EntityToDetailDto(questionnaire);

        return Success(result);
    }
}
