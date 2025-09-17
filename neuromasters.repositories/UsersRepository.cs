using Microsoft.AspNetCore.Identity;
using neuromasters.borders.Dtos;
using neuromasters.borders.Dtos.Auth;
using neuromasters.borders.Entities;
using neuromasters.borders.Repositories;

namespace neuromasters.repositories;

public class UsersRepository : IUsersRepository
{
    private readonly UserManager<User> _userManager;

    public UsersRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user is null ? null : MapToDto(user);
    }

    public async Task<UserDto?> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user is null ? null : MapToDto(user);
    }

    public async Task<string> CreateUserAsync(CreateUserDto userDto)
    {
        var identityUser = new User
        {
            UserName = userDto.Email,
            FullName = userDto.FullName,
            Email = userDto.Email,
            EmailConfirmed = userDto.EmailConfirmed,
            PhoneNumber = userDto.PhoneNumber,
            PhoneNumberConfirmed = userDto.PhoneNumberConfirmed,
        };

        // aqui usamos a senha de dentro do CreateUserDto
        var result = await _userManager.CreateAsync(identityUser, userDto.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException(
                $"Erro ao criar usuário: {string.Join(", ", result.Errors.Select(e => e.Description))}"
            );

        return identityUser.Id;
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto(
            user.Id,
            user.FullName ?? string.Empty,
            user.Email ?? string.Empty,
            user.PhoneNumber ?? string.Empty,
            user.EmailConfirmed,
            user.PhoneNumberConfirmed,
            DateTime.UtcNow // Identity não traz CreatedAt por padrão
        );
    }
}