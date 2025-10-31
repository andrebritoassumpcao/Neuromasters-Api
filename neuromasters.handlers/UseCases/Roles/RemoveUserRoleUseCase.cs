using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Dtos.Roles;
using neuromasters.borders.Repositories;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Roles;

namespace neuromasters.handlers.UseCases.Roles;

public class RemoveRoleUseCase(
    ILogger<RemoveRoleUseCase> logger,
    IValidator<AssignRoleRequest> validator,
    IRolesRepository rolesRepository,
    IUsersRepository usersRepository)
    : UseCase<AssignRoleRequest, UserRolesDto>(logger, validator),
    IRemoveRoleUseCase
{
    protected override async Task<UseCaseResponse<UserRolesDto>> OnExecute(AssignRoleRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var user = await usersRepository.GetUserByIdAsync(request.UserId);
        if (user is null)
            return BadRequest(new ErrorMessage("54.1", "Usuário não encontrado"));

        var role = await rolesRepository.GetRoleByIdAsync(request.RoleId);
        if (string.IsNullOrWhiteSpace(role.Name))
            return BadRequest(new ErrorMessage("54.2", "Role não existe no sistema"));

        var userHasRole = await rolesRepository.UserHasRoleAsync(request.UserId, role.Name);
        if (!userHasRole)
            return BadRequest(new ErrorMessage("54.3", "Usuário não possui essa role"));

        var removed = await rolesRepository.RemoveRoleFromUserAsync(request.UserId, role.Name);
        if (!removed)
            return BadRequest(new ErrorMessage("54.4", "Erro ao remover role do usuário"));

        var updatedRoles = (await rolesRepository.GetUserRolesAsync(request.UserId))
            .ToList();

        var response = new UserRolesDto(
            user.Id,
            user.Email ?? string.Empty,
            user.FullName ?? string.Empty,
            updatedRoles
        );

        return Success(response);
    }
}


