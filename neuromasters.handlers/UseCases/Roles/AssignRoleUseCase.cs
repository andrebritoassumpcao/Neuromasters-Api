using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Dtos.Roles;
using neuromasters.borders.Repositories;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.handlers.UseCases.Roles
{
    public class AssignRoleUseCase(
    ILogger<AssignRoleUseCase> logger,
    IValidator<AssignRoleRequest> validator,
    IRolesRepository rolesRepository,
    IUsersRepository usersRepository,
    IRoleAdapter adapter) :
    UseCase<AssignRoleRequest, RoleAssignmentDto>(logger, validator),
    IAssignRoleUseCase
    {
        protected override async Task<UseCaseResponse<RoleAssignmentDto>> OnExecute(AssignRoleRequest request)
        {
            await validator.ValidateAndThrowAsync(request);

            var user = await usersRepository.GetUserByIdAsync(request.UserId);
            if (user is null)
                return BadRequest(new ErrorMessage("51.1", "Usuário não encontrado"));

            var roleExists = await rolesRepository.RoleExistsAsync(request.RoleName);
            if (!roleExists)
                return BadRequest(new ErrorMessage("51.2", "Role não existe no sistema"));

            var userAlreadyHasRole = await rolesRepository.UserHasRoleAsync(request.UserId, request.RoleName);
            if (userAlreadyHasRole)
                return BadRequest(new ErrorMessage("51.3", "Usuário já possui essa role"));

            var assignmentResult = await rolesRepository.AssignRoleToUserAsync(request.UserId, request.RoleName);
            if (!assignmentResult)
                return BadRequest(new ErrorMessage("51.4", "Erro ao atribuir role ao usuário"));

            var response = adapter.ToRoleAssignmentDto(user, request.RoleName);

            return Persisted(response, $"{response.UserId}-{response.RoleName}");
        }
    }
}
