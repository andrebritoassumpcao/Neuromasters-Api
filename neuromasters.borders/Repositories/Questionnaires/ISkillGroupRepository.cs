using neuromasters.borders.Dtos.Questionnaires.SkillGroups;

namespace neuromasters.borders.Repositories.Questionnaires
{
    public interface ISkillGroupRepository
    {
        Task<IEnumerable<SkillGroupDto>> GetAllAsync();
        Task<SkillGroupDto?> GetByCodeAsync(string code);
        Task<SkillGroupDto> AddAsync(SkillGroupDto dto);
        Task<SkillGroupDto> UpdateAsync(SkillGroupDto dto);
        Task<bool> DeleteAsync(string code);
        Task<bool> ExistsAsync(string code);
    }
}
