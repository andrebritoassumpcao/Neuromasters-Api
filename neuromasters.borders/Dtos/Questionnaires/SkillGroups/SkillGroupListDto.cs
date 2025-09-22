namespace neuromasters.borders.Dtos.Questionnaires.SkillGroups;

public record SkillGroupListDto(IEnumerable<SkillGroupDto> SkillGroups, int TotalCount);
