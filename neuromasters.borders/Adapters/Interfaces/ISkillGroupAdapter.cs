using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Entities.Questionnaires;

namespace neuromasters.borders.Adapters.Interfaces;

public interface ISkillGroupAdapter
{
    SkillGroupDto ToDto(SkillGroup entity);
    SkillGroup ToEntity(SkillGroupDto dto);
}
