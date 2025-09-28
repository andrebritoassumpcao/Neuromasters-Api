using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.DefaultAswers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.UseCases.Questionnaires.DefaultAswers
{
    public class DeleteDefaultAnswerUseCase(
    ILogger<DeleteDefaultAnswerUseCase> logger,
    IValidator<GetDefaultAnswerRequest> validator,
    IDefaultAnswerRepository repository)
    : UseCase<GetDefaultAnswerRequest, bool>(logger, validator),
      IDeleteDefaultAnswerUseCase
    {
        protected override async Task<UseCaseResponse<bool>> OnExecute(GetDefaultAnswerRequest request)
        {
            await validator.ValidateAndThrowAsync(request);

            var exists = await repository.ExistsAsync(request.Id);
            if (!exists)
                return BadRequest(new ErrorMessage("DA.03", "Resposta padrão não encontrada"));

            var deleted = await repository.DeleteAsync(request.Id);

            return Success(deleted);
        }
    }
}
