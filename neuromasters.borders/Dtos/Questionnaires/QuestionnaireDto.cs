namespace neuromasters.borders.Dtos.Questionnaires;

public record QuestionnaireDto(
        int Id,
        string Name,
        string? Description,
        string Status,
        DateTime CreatedAt
    );

