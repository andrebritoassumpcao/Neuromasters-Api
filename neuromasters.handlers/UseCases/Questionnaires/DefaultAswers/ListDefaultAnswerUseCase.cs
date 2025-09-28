using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.DefaultAswers;

namespace neuromasters.handlers.UseCases.Questionnaires.DefaultAswers;

public class ListDefaultAnswerUseCase(
    ILogger<ListDefaultAnswerUseCase> logger,
    IValidator<ListDefaultAnswerRequest> validator,
    IDefaultAnswerRepository repository)
    : UseCase<ListDefaultAnswerRequest, IEnumerable<DefaultAnswerDto>>(logger, validator),
      IListDefaultAnswerUseCase
{
    protected override async Task<UseCaseResponse<IEnumerable<DefaultAnswerDto>>> OnExecute(ListDefaultAnswerRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var answers = await repository.GetByQuestionnaireIdAsync(request.QuestionnaireId);
        if (!answers.Any())
            return BadRequest(new ErrorMessage("DA.01", "Nenhuma resposta padrão encontrada para este questionário."));

        return Success(answers);
    }
}
