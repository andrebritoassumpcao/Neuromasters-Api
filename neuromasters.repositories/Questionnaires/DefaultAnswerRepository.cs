using Microsoft.EntityFrameworkCore;
using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using neuromasters.borders.Entities.Questionnaires;
using neuromasters.borders.Repositories.Questionnaires;

namespace neuromasters.repositories.Questionnaires;

public class DefaultAnswerRepository : IDefaultAnswerRepository
{
    private readonly AppDbContext _context;

    public DefaultAnswerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DefaultAnswer> AddAsync(DefaultAnswer entity)
    {
        _context.DefaultAnswers.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<DefaultAnswerDto>> GetByQuestionnaireIdAsync(int questionnaireId)
    {
        var defaultAnswers = await _context.DefaultAnswers
            .AsNoTracking()
            .Where(da => da.QuestionnaireId == questionnaireId)
            .OrderBy(da => da.Label)
            .ToListAsync();

        return defaultAnswers.Select(MapToDto);
    }

    public async Task<DefaultAnswerDto?> GetByIdAsync(int id)
    {
        var defaultAnswer = await _context.DefaultAnswers
            .AsNoTracking()
            .FirstOrDefaultAsync(da => da.Id == id);

        return defaultAnswer is null ? null : MapToDto(defaultAnswer);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.DefaultAnswers.AnyAsync(da => da.Id == id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var defaultAnswer = await _context.DefaultAnswers
            .FirstOrDefaultAsync(da => da.Id == id);

        if (defaultAnswer is null) return false;

        _context.DefaultAnswers.Remove(defaultAnswer);
        return await _context.SaveChangesAsync() > 0;
    }

    private static DefaultAnswerDto MapToDto(DefaultAnswer entity)
        => new(entity.Id, entity.QuestionnaireId, entity.Label, entity.Color);
}
