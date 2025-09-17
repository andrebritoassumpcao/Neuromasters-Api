using Microsoft.AspNetCore.Identity;
using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Dtos;
using neuromasters.borders.Dtos.Auth;

namespace neuromasters.borders.Adapters;

public class UserAdapter : IUserAdapter
{
    public CreateUserDto ToCreateUserDto(RegisterRequest request)
    {
        return new CreateUserDto(
            request.FullName,
            request.Email,
            request.Password,
            request.PhoneNumber,
            true,
            true
        );
    }

    public UserDto ToUserDto(IdentityUser user)
    {
        return new UserDto(
            user.Id,
            user.UserName ?? string.Empty,
            user.Email ?? string.Empty,
            user.PhoneNumber ?? string.Empty,
            user.EmailConfirmed,
            user.PhoneNumberConfirmed,
            DateTime.UtcNow
        );
    }
}
