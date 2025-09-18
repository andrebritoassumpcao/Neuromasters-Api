using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Dtos.Roles;
using neuromasters.borders.Repositories;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Roles;

namespace neuromasters.handlers.UseCases.Roles;

public class CreateRoleUseCase(
ILogger<CreateRoleUseCase> logger,
IValidator<CreateRoleRequest> validator,
IRolesRepository repository) :
UseCase<CreateRoleRequest, RoleDto>(logger, validator),
ICreateRoleUseCase
{
    protected override async Task<UseCaseResponse<RoleDto>> OnExecute(CreateRoleRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var roleExists = await repository.RoleExistsAsync(request.RoleName);
        if (roleExists)
            return BadRequest(new ErrorMessage("50.1", "Role já existe no sistema"));

        var created = await repository.CreateRoleAsync(request.RoleName);
        if (!created)
            return BadRequest(new ErrorMessage("50.2", "Erro ao criar a role"));

        var response = await repository.GetRoleByNameAsync(request.RoleName);

        return response is null
            ? BadRequest(new ErrorMessage("50.3", "Erro ao recuperar role criada"))
            : Persisted(response, response.Id);
    }
}
