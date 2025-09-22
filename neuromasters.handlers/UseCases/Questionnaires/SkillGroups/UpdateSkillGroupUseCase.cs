using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.SkillGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.UseCases.Questionnaires.SkillGroups
{
    public class UpdateSkillGroupUseCase(
    ILogger<UpdateSkillGroupUseCase> logger,
    IValidator<UpdateSkillGroupRequest> validator,
    ISkillGroupRepository repository)
    : UseCase<UpdateSkillGroupRequest, SkillGroupDto>(logger, validator),
      IUpdateSkillGroupUseCase
    {
        protected override async Task<UseCaseResponse<SkillGroupDto>> OnExecute(UpdateSkillGroupRequest request)
        {
            await validator.ValidateAndThrowAsync(request);

            var exists = await repository.ExistsAsync(request.Code);
            if (!exists)
                return BadRequest(new ErrorMessage("SG.03", "Grupo de habilidade não encontrado"));

            var dto = new SkillGroupDto(request.Code, request.Description);
            var updated = await repository.UpdateAsync(dto);

            return Success(updated);
        }
    }
}
