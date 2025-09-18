using neuromasters.borders.Dtos.Roles;
using neuromasters.borders.Repositories;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Roles;

namespace neuromasters.handlers.UseCases.Roles;


public class ListRolesUseCase(IRolesRepository repository) : IListRolesUseCase
{
    public async Task<UseCaseResponse<RolesListDto>> Execute()
    {
        var roles = await repository.GetAllRolesAsync();
        var rolesList = roles.ToList();

        var response = new RolesListDto(rolesList, rolesList.Count);

        return Success(response);
    }
    protected UseCaseResponse<RolesListDto> Success(RolesListDto response)
    => new()
    {
        Status = UseCaseResponseKind.Success,
        Result = response
    };
}
