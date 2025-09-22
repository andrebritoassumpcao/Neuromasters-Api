using Microsoft.EntityFrameworkCore;
using neuromasters.borders.Adapters;
using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Entities.Questionnaires;
using neuromasters.borders.Repositories.Questionnaires;

namespace neuromasters.repositories.Questionnaires;

public class SkillGroupRepository : ISkillGroupRepository
{
    private readonly AppDbContext _context;

    public SkillGroupRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SkillGroupDto>> GetAllAsync()
    {
        var entities = await _context.SkillGroups.AsNoTracking().ToListAsync();
        return entities.Select(e => new SkillGroupDto(e.Code, e.Description));
    }

    public async Task<SkillGroupDto?> GetByCodeAsync(string code)
    {
        var entity = await _context.SkillGroups.AsNoTracking()
                            .FirstOrDefaultAsync(g => g.Code == code);

        return entity is null ? null : new SkillGroupDto(entity.Code, entity.Description);
    }

    public async Task<SkillGroupDto> AddAsync(SkillGroupDto dto)
    {
        var entity = new SkillGroup
        {
            Code = dto.Code,
            Description = dto.Description
        };

        _context.SkillGroups.Add(entity);
        await _context.SaveChangesAsync();

        return new SkillGroupDto(entity.Code, entity.Description);
    }

    public async Task<SkillGroupDto> UpdateAsync(SkillGroupDto dto)
    {
        var entity = await _context.SkillGroups
            .FirstOrDefaultAsync(g => g.Code == dto.Code);

        if (entity is null)
            throw new KeyNotFoundException($"SkillGroup with code {dto.Code} not found.");

        entity.Description = dto.Description;

        _context.SkillGroups.Update(entity);
        await _context.SaveChangesAsync();

        return new SkillGroupDto(entity.Code, entity.Description);
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var entity = await _context.SkillGroups.FirstOrDefaultAsync(g => g.Code == code);
        if (entity is null) return false;

        _context.SkillGroups.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ExistsAsync(string code)
    {
        return await _context.SkillGroups.AnyAsync(g => g.Code == code);
    }
}
