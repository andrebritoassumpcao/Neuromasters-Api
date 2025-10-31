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
    public class GetUserRolesUseCase(
        ILogger<GetUserRolesUseCase> logger,
        IUsersRepository usersRepository,
        IValidator<GetUserRolesRequest> validator,
        IRolesRepository rolesRepository) :
        UseCase<GetUserRolesRequest, UserRolesDto>(logger, validator),
        IGetUserRolesUseCase
    {
        protected override async Task<UseCaseResponse<UserRolesDto>> OnExecute(GetUserRolesRequest request)
        {
            await validator.ValidateAndThrowAsync(request);

            var user = await usersRepository.GetUserByIdAsync(request.UserId);
            if (user is null)
                return BadRequest(new ErrorMessage("53.1", "Usuário não encontrado"));

            var roles = (await rolesRepository.GetUserRolesAsync(request.UserId)).ToList();

            if (roles.Count == 0)
                return BadRequest(new ErrorMessage("53.2", "Usuário sem roles atribuídas"));

            var response = new UserRolesDto(
                user.Id,
                user.Email ?? string.Empty,
                user.FullName ?? string.Empty,
                roles
            );

            return Success(response);
        }
    }
}
