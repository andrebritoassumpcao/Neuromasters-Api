using neuromasters.borders.Dtos.Questionnaires.Forms;
using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Entities.Questionnaires;
using neuromasters.borders.Repositories.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace neuromasters.repositories.Questionnaires
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly AppDbContext _context;

        public QuestionnaireRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Questionnaire> AddAsync(Questionnaire entity)
        {
            _context.Questionnaires.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Questionnaire> UpdateAsync(Questionnaire entity)
        {
            _context.Questionnaires.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<QuestionnaireDto>> GetAllAsync()
        {
            var questionnaires = await _context.Questionnaires
                .AsNoTracking()
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();

            return questionnaires.Select(MapToDto);
        }

        public async Task<Questionnaire?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Questionnaires
                .Include(q => q.Sections)
                    .ThenInclude(s => s.Questions)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<QuestionnaireDto?> GetByIdAsync(int id)
        {
            var questionnaire = await _context.Questionnaires
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);

            return questionnaire is null ? null : MapToDto(questionnaire);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Questionnaires.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var questionnaire = await _context.Questionnaires
                .Include(q => q.Sections)
                    .ThenInclude(s => s.Questions)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (questionnaire is null) return false;

            _context.Questionnaires.Remove(questionnaire);
            return await _context.SaveChangesAsync() > 0;
        }

        private static QuestionnaireDto MapToDto(Questionnaire entity)
            => new(entity.Id, entity.Name, entity.Description, entity.Status, entity.CreatedAt);

    }
}
