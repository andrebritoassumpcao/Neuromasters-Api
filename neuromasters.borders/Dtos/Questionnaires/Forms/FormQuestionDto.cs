namespace neuromasters.borders.Dtos.Questionnaires.Forms;

public record FormQuestionDto(
    int Id,
    string Text,
    string? Observations,
    int Order
);
