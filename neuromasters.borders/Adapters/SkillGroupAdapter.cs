using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Entities.Questionnaires;

namespace neuromasters.borders.Adapters;

public class SkillGroupAdapter : ISkillGroupAdapter
{
    public SkillGroupDto ToDto(SkillGroup entity)
        => new SkillGroupDto(entity.Code, entity.Description);

    public SkillGroup ToEntity(SkillGroupDto dto)
        => new SkillGroup
        {
            Code = dto.Code,
            Description = dto.Description
        };
}
