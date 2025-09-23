namespace neuromasters.borders.Dtos.Questionnaires.Forms;

public record FormSectionDto(
        int Id,
        string Name,
        int Order,
        IEnumerable<FormQuestionDto> Questions
    );
