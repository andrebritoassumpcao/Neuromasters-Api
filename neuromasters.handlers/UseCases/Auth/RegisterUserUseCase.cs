using FluentValidation;
using Microsoft.Extensions.Logging;
using neuromasters.borders.Dtos;
using neuromasters.borders.Dtos.Auth;
using neuromasters.borders.Shared;
using neuromasters.borders.Repositories;
using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.UseCases.Auth;

namespace neuromasters.handlers.UseCases.Auth;

public class RegisterUserUseCase(
 ILogger<RegisterUserUseCase> logger,
 IValidator<RegisterRequest> validator,
 IUsersRepository repository,
 IRolesRepository rolesRepository,
 IUserAdapter adapter) :
 UseCase<RegisterRequest, UserDto>(logger, validator),
 IRegisterUserUseCase
{
    protected override async Task<UseCaseResponse<UserDto>> OnExecute(RegisterRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var alreadyExistUser = await repository.GetUserByEmailAsync(request.Email);
        if (alreadyExistUser is not null)
            return BadRequest(new ErrorMessage("40.1", "Esse email já está cadastrado"));

        var user = adapter.ToCreateUserDto(request);
        var createdUserId = await repository.CreateUserAsync(user);

        var defaultRole = "Cliente";
        var roleExists = await rolesRepository.RoleExistsAsync(defaultRole);
        if (!roleExists)
        {
            await rolesRepository.CreateRoleAsync(defaultRole);
        }

        var assignedRole = await rolesRepository.AssignRoleToUserAsync(createdUserId, defaultRole);
        if (!assignedRole)
            return BadRequest(new ErrorMessage("40.3", "Erro ao atribuir role padrão ao usuário"));

        var response = await repository.GetUserByIdAsync(createdUserId);
        return response is null
            ? BadRequest(new ErrorMessage("40.2", "Erro"))
            : Persisted(response, response.Id.ToString());
    }
}
