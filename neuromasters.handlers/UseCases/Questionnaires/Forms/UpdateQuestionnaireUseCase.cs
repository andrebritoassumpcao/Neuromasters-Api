using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.UseCases.Questionnaires.Forms;

public class UpdateQuestionnaireUseCase(
  ILogger<UpdateQuestionnaireUseCase> logger,
  IQuestionnaireAdapter adapter,
  IValidator<UpdateQuestionnaireRequest> validator,
  IQuestionnaireRepository repository)
  : UseCase<UpdateQuestionnaireRequest, QuestionnaireDetailDto>(logger, validator),
    IUpdateQuestionnaireUseCase
{
    protected override async Task<UseCaseResponse<QuestionnaireDetailDto>> OnExecute(UpdateQuestionnaireRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var existing = await repository.GetByIdWithDetailsAsync(request.Id);
        if (existing is null)
            return BadRequest(new ErrorMessage("QT.01", "Questionário não encontrado"));

        var entity = adapter.UpdateRequestToEntity(request, existing);
        var updated = await repository.UpdateAsync(entity);
        var dto = adapter.EntityToDetailDto(updated);

        return Success(dto);
    }
}
