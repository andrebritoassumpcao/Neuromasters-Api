namespace neuromasters.borders.Dtos.Questionnaires.Forms;

public record CreateFormSectionRequest(
    string Name,
    int Order,
    IEnumerable<CreateFormQuestionRequest> Questions
);
