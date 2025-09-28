namespace neuromasters.borders.Dtos.Questionnaires.DefaultAswers;

public record CreateDefaultAnswerRequest(
 int QuestionnaireId,
 string Label,
 string Color
);
