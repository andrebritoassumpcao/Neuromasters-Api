using neuromasters.borders.Entities.Enums;

namespace neuromasters.borders.Dtos.Questionnaires;

public record QuestionnaireDto(
        int Id,
        string Name,
        string? Description,
        QuestionnaireStatusEnum Status,
        DateTime CreatedAt
    );

