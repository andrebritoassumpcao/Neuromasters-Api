using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Shared;

namespace neuromasters.borders.UseCases.Questionnaires.SkillGroups;

public interface IDeleteSkillGroupUseCase : IUseCase<GetSkillGroupRequest, bool>;
