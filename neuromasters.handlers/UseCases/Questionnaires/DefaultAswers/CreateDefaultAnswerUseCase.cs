using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.DefaultAswers;

namespace neuromasters.handlers.UseCases.Questionnaires.DefaultAswers
{
    public class CreateDefaultAnswerUseCase(
    ILogger<CreateDefaultAnswerUseCase> logger,
    IValidator<CreateDefaultAnswerRequest> validator,
    IDefaultAnswerAdapter adapter,
    IDefaultAnswerRepository repository)
    : UseCase<CreateDefaultAnswerRequest, DefaultAnswerDto>(logger, validator),
      ICreateDefaultAnswerUseCase
    {
        protected override async Task<UseCaseResponse<DefaultAnswerDto>> OnExecute(CreateDefaultAnswerRequest request)
        {
            await validator.ValidateAndThrowAsync(request);

            var entity = adapter.CreateRequestToEntity(request);
            var created = await repository.AddAsync(entity);
            var dto = adapter.EntityToDto(created);

            return Success(dto);
        }
    }
}
