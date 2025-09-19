using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Shared;
using neuromasters.borders.Dtos.Auth;
using neuromasters.borders.Repositories;
using global::neuromasters.borders.Dtos.Roles;
using global::neuromasters.borders.Repositories;
using global::neuromasters.borders.Shared;
using global::neuromasters.borders.UseCases.Roles;


namespace neuromasters.handlers.UseCases.Roles
{

    public class GetUserRoleUseCase(
        ILogger<GetUserRoleUseCase> logger,
        IUsersRepository usersRepository,
        IValidator<GetUserRolesRequest> validator,
        IRolesRepository rolesRepository) :
        UseCase<GetUserRolesRequest, UserRoleDto>(logger, validator),
        IGetUserRoleUseCase
    {
        protected override async Task<UseCaseResponse<UserRoleDto>> OnExecute(GetUserRolesRequest request)
        {

            var user = await usersRepository.GetUserByIdAsync(request.UserId);
            if (user is null)
                return BadRequest(new ErrorMessage("53.1", "Usuário não encontrado"));

            var role = await rolesRepository.GetUserRoleAsync(request.UserId);
            if (string.IsNullOrEmpty(role))
                return BadRequest(new ErrorMessage("53.2", "Usuário sem role atribuída"));

            var response = new UserRoleDto(
                user.Id,
                user.Email,
                user.FullName,
                role
            );

            return Success(response);
        }
    }
}
