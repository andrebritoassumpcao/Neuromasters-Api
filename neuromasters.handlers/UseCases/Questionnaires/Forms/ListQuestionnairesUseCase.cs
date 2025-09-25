using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.Form;

namespace neuromasters.handlers.UseCases.Questionnaires.Forms
{
    public class ListQuestionnairesUseCase(IQuestionnaireRepository repository) : IListQuestionnairesUseCase
    {
        public async Task<UseCaseResponse<QuestionnaireListDto>> Execute()
        {
            var result = await repository.GetAllAsync();
            var resultList = result.ToList();
            var response = new QuestionnaireListDto(resultList, resultList.Count);

            return Success(response);
        }
        protected UseCaseResponse<QuestionnaireListDto> Success(QuestionnaireListDto response)
        => new()
        {
            Status = UseCaseResponseKind.Success,
            Result = response
        };
    }
}
