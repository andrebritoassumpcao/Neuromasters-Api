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
    public class DeleteSkillGroupUseCase(
    ILogger<DeleteSkillGroupUseCase> logger,
    IValidator<GetSkillGroupRequest> validator,
    ISkillGroupRepository repository)
    : UseCase<GetSkillGroupRequest, bool>(logger, validator),
      IDeleteSkillGroupUseCase
    {
        protected override async Task<UseCaseResponse<bool>> OnExecute(GetSkillGroupRequest request)
        {
            await validator.ValidateAndThrowAsync(request);

            var exists = await repository.ExistsAsync(request.Code);
            if (!exists)
                return BadRequest(new ErrorMessage("SG.04", "Grupo de habilidade não encontrado"));

            var deleted = await repository.DeleteAsync(request.Code);

            return Success(deleted);
        }
    }
}
