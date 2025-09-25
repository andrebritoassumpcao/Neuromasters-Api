using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Dtos.Questionnaires.Forms;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.Form;
using neuromasters.borders.UseCases.Questionnaires.SkillGroups;
using neuromasters.handlers.UseCases.Questionnaires.SkillGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.UseCases.Questionnaires.Forms;

public class DeleteQuestionnaireUseCase(
    ILogger<DeleteQuestionnaireUseCase> logger,
    IValidator<GetQuestionnaireRequest> validator,
    IQuestionnaireRepository repository)
    : UseCase<GetQuestionnaireRequest, bool>(logger, validator), IDeleteQuestionnaireUseCase
{
    protected override async Task<UseCaseResponse<bool>> OnExecute(GetQuestionnaireRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var exists = await repository.ExistsAsync(request.Id);
        if (!exists)
            return BadRequest(new ErrorMessage("QT.01", "Grupo de habilidade não encontrado"));

        var deleted = await repository.DeleteAsync(request.Id);

        return Success(deleted);
    }
}
