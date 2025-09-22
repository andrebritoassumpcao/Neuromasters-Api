namespace neuromasters.borders.Dtos.Questionnaires;

public record CompetenceDto(
    int Code,
    string GroupCode,
    string Description,
    int Sequence
);

public record DegreeCompetenceDto(
    int Code,
    string Description
);

public record QuestionnaireDto(
    int Id,
    string Name,
    string? Description,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record FormSectionDto(
    int Id,
    int FormId,
    string Name,
    string? SkillGroupCode,
    int Order
);

public record FormQuestionDto(
    int Id,
    int SectionId,
    string Text,
    int? CompetenceCode,
    string? Observations,
    int Order
);

public record FormResponseDto(
    int Id,
    int FormId,
    string UserId,
    int QuestionId,
    int DegreeCode,
    string? Observation,
    DateTime CreatedAt
);

