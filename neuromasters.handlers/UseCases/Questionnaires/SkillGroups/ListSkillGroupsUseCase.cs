using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Repositories.Questionnaires;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.SkillGroups;

namespace neuromasters.handlers.UseCases.Questionnaires.SkillGroups;

public class ListSkillGroupsUseCase(ISkillGroupRepository repository) : IListSkillGroupsUseCase
{
    public async Task<UseCaseResponse<SkillGroupListDto>> Execute()
    {
        var result = await repository.GetAllAsync();
        var resultList = result.ToList();
        var response = new SkillGroupListDto(resultList, resultList.Count);

        return Success(response);
    }
    protected UseCaseResponse<SkillGroupListDto> Success(SkillGroupListDto response)
    => new()
    {
        Status = UseCaseResponseKind.Success,
        Result = response
    };

}