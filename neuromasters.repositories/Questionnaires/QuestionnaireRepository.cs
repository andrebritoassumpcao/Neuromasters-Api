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

        public async Task<QuestionnaireDetailDto> AddAsync(CreateQuestionnaireRequest request)
        {
            var questionnaire = new Questionnaire
            {
                Name = request.Name,
                Description = request.Description,
                Status = "draft",
                CreatedAt = DateTime.UtcNow
            };

            _context.Questionnaires.Add(questionnaire);
            await _context.SaveChangesAsync(); 

            var sections = new List<FormSection>();
            foreach (var sectionRequest in request.Sections)
            {
                var section = new FormSection
                {
                    FormId = questionnaire.Id,
                    Name = sectionRequest.Name,
                    Order = sectionRequest.Order
                };

                _context.FormSections.Add(section);
                await _context.SaveChangesAsync(); 

                var questions = new List<FormQuestion>();
                foreach (var questionRequest in sectionRequest.Questions)
                {
                    var question = new FormQuestion
                    {
                        SectionId = section.Id,
                        Text = questionRequest.Text,
                        Observations = questionRequest.Observations,
                        Order = questionRequest.Order
                    };

                    _context.FormQuestions.Add(question);
                    questions.Add(question);
                }

                await _context.SaveChangesAsync();

                section.Questions = questions;
                sections.Add(section);
            }

            questionnaire.Sections = sections;

            return MapToDetailDto(questionnaire);
        }

        public async Task<IEnumerable<QuestionnaireDto>> GetAllAsync()
        {
            var questionnaires = await _context.Questionnaires
                .AsNoTracking()
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();

            return questionnaires.Select(MapToDto);
        }

        public async Task<QuestionnaireDetailDto?> GetByIdWithDetailsAsync(int id)
        {
            var questionnaire = await _context.Questionnaires
                .AsNoTracking()
                .Include(q => q.Sections.OrderBy(s => s.Order))
                    .ThenInclude(s => s.Questions.OrderBy(q => q.Order))
                .FirstOrDefaultAsync(q => q.Id == id);

            return questionnaire is null ? null : MapToDetailDto(questionnaire);
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

        private static QuestionnaireDetailDto MapToDetailDto(Questionnaire entity)
            => new(
                entity.Id,
                entity.Name,
                entity.Description,
                entity.Status,
                entity.CreatedAt,
                entity.Sections?.Select(MapToSectionDto) ?? Enumerable.Empty<FormSectionDto>()
            );

        private static FormSectionDto MapToSectionDto(FormSection entity)
            => new(
                entity.Id,
                entity.Name,
                entity.Order,
                entity.Questions?.Select(MapToQuestionDto) ?? Enumerable.Empty<FormQuestionDto>()
            );

        private static FormQuestionDto MapToQuestionDto(FormQuestion entity)
            => new(entity.Id, entity.Text, entity.Observations, entity.Order);
    }
}
