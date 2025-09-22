using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.SkillGroups;

namespace neuromasters.handlers.UseCases.Questionnaires.SkillGroups;

public class CreateSkillGroupUseCase(
        ILogger<CreateSkillGroupUseCase> logger,
        IValidator<CreateSkillGroupRequest> validator,
        ISkillGroupRepository repository) :
        UseCase<CreateSkillGroupRequest, SkillGroupDto>(logger, validator),
        ICreateSkillGroupUseCase
{
    protected override async Task<UseCaseResponse<SkillGroupDto>> OnExecute(CreateSkillGroupRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var exists = await repository.ExistsAsync(request.Code);
        if (exists)
            return BadRequest(new ErrorMessage("SG.01", "Grupo de habilidade já existe com este código"));

        var dto = new SkillGroupDto(request.Code, request.Description);
        var created = await repository.AddAsync(dto);

        return Persisted(created, created.Code);
    }
}
