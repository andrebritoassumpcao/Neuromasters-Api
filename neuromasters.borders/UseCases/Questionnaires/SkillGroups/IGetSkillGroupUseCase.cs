using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Shared;

namespace neuromasters.borders.UseCases.Questionnaires.SkillGroups;

public interface IGetSkillGroupUseCase : IUseCase<GetSkillGroupRequest, SkillGroupDto>;
