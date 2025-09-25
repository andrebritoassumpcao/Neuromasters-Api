using neuromasters.borders.Dtos.Questionnaires.Forms;
using neuromasters.borders.Dtos.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Entities.Questionnaires;

namespace neuromasters.borders.Adapters;

public class QuestionnaireAdapter : IQuestionnaireAdapter
{
    public QuestionnaireDetailDto EntityToDetailDto(Questionnaire entity)
    {
        return new QuestionnaireDetailDto(
            Id: entity.Id,
            Name: entity.Name,
            Description: entity.Description,
            Status: entity.Status,
            CreatedAt: entity.CreatedAt,
            Sections: entity.Sections?.Select(section => new FormSectionDto(
                Id: section.Id,
                Name: section.Name,
                Order: section.Order,
                Questions: section.Questions?.Select(q => new FormQuestionDto(
                    Id: q.Id,
                    Text: q.Text,
                    Observations: q.Observations,
                    Order: q.Order
                )).ToList() ?? new List<FormQuestionDto>()
            )).ToList() ?? new List<FormSectionDto>()
        );
    }

    public Questionnaire UpdateRequestToEntity(UpdateQuestionnaireRequest request, Questionnaire existingEntity)
    {
        existingEntity.Name = request.Name;
        existingEntity.Description = request.Description;
        existingEntity.Status = request.Status;
        existingEntity.UpdatedAt = DateTime.UtcNow;

        foreach (var sectionRequest in request.Sections)
        {
            var section = existingEntity.Sections
                .FirstOrDefault(s => s.Id == sectionRequest.Id);

            if (section is null)
            {
                section = new FormSection
                {
                    FormId = existingEntity.Id
                };
                existingEntity.Sections.Add(section);
            }

            section.Name = sectionRequest.Name;
            section.Order = sectionRequest.Order;

            foreach (var questionRequest in sectionRequest.Questions)
            {
                var question = section.Questions
                    .FirstOrDefault(q => q.Id == questionRequest.Id);

                if (question is null)
                {
                    question = new FormQuestion
                    {
                        SectionId = section.Id
                    };
                    section.Questions.Add(question);
                }

                question.Text = questionRequest.Text;
                question.Observations = questionRequest.Observations;
                question.Order = questionRequest.Order;
            }
        }

        return existingEntity;
    }

    public Questionnaire CreateRequestToEntity(CreateQuestionnaireRequest request)
    {
        return new Questionnaire
        {
            Name = request.Name,
            Description = request.Description,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow,
            Sections = request.Sections.Select(sectionRequest => new FormSection
            {
                Name = sectionRequest.Name,
                Order = sectionRequest.Order,
                Questions = sectionRequest.Questions.Select(questionRequest => new FormQuestion
                {
                    Text = questionRequest.Text,
                    Observations = questionRequest.Observations,
                    Order = questionRequest.Order
                }).ToList()
            }).ToList()
        };
    }
}
