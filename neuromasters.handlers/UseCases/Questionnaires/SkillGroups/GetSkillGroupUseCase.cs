using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.UseCases.Questionnaires.SkillGroups;
using neuromasters.borders.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.UseCases.Questionnaires.SkillGroups;

public class GetSkillGroupUseCase(
 ILogger<GetSkillGroupUseCase> logger,
 IValidator<GetSkillGroupRequest> validator,
 ISkillGroupRepository repository)
 : UseCase<GetSkillGroupRequest, SkillGroupDto>(logger, validator),
   IGetSkillGroupUseCase
{
    protected override async Task<UseCaseResponse<SkillGroupDto>> OnExecute(GetSkillGroupRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var skillGroup = await repository.GetByCodeAsync(request.Code);
        if (skillGroup is null)
            return BadRequest(new ErrorMessage("SG.02", "Grupo de habilidade não encontrado"));

        return Success(skillGroup);
    }
}
