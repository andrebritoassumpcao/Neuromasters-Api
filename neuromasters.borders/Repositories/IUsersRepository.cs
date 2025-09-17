using neuromasters.borders.Dtos;
using neuromasters.borders.Dtos.Auth;

namespace neuromasters.borders.Repositories;

public interface IUsersRepository
{
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto?> GetUserByIdAsync(string id);
    Task<string> CreateUserAsync(CreateUserDto user);
}
