using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Dtos.Questionnaires.Forms;
using neuromasters.borders.Shared;

namespace neuromasters.borders.UseCases.Questionnaires.Form;

public interface IGetQuestionnaireUseCase : IUseCase<GetQuestionnaireRequest, QuestionnaireDetailDto>;
