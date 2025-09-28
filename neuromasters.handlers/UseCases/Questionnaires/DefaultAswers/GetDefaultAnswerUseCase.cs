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

namespace neuromasters.handlers.UseCases.Questionnaires.DefaultAswers;

public class GetDefaultAnswerUseCase(
 ILogger<GetDefaultAnswerUseCase> logger,
 IValidator<GetDefaultAnswerRequest> validator,
 IDefaultAnswerRepository repository)
 : UseCase<GetDefaultAnswerRequest, DefaultAnswerDto?>(logger, validator),
   IGetDefaultAnswerUseCase
{
    protected override async Task<UseCaseResponse<DefaultAnswerDto?>> OnExecute(GetDefaultAnswerRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var answer = await repository.GetByIdAsync(request.Id);
        if (answer is null)
            return BadRequest(new ErrorMessage("DA.02", "Resposta padrão não encontrada"));

        return Success(answer);
    }
}
