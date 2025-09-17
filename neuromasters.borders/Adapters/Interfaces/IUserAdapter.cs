using neuromasters.borders.Dtos.Auth;
using neuromasters.borders.Dtos;
using Microsoft.AspNetCore.Identity;

namespace neuromasters.borders.Adapters.Interfaces;
public interface IUserAdapter
{
    UserDto ToUserDto(IdentityUser user);
    CreateUserDto ToCreateUserDto(RegisterRequest request);
}
