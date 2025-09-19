using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using neuromasters.borders.Dtos.Auth;
using neuromasters.borders.Entities;
using neuromasters.borders.Repositories;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace neuromasters.handlers.UseCases.Auth
{
    public class LoginUseCase(
     ILogger<LoginUseCase> logger,
     IValidator<LoginRequest> validator,
     UserManager<User> userManager,
     IUsersRepository usersRepository,
     IRolesRepository rolesRepository,
     IConfiguration configuration) :
     UseCase<LoginRequest, LoginResponse>(logger, validator),
     ILoginUseCase
    {
        protected override async Task<UseCaseResponse<LoginResponse>> OnExecute(LoginRequest request)
        {
            await validator.ValidateAndThrowAsync(request);

            // Buscar usuário por email
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return BadRequest(new ErrorMessage("60.1", "Email ou senha inválidos"));

            // Verificar senha
            var isValidPassword = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
                return BadRequest(new ErrorMessage("60.2", "Email ou senha inválidos"));

            var userRole = await rolesRepository.GetUserRoleAsync(user.Id);

            var token = await GenerateJwtToken(user, userRole);

            var userDto = await usersRepository.GetUserByIdAsync(user.Id);
            if (userDto is null)
                return BadRequest(new ErrorMessage("60.3", "Erro ao recuperar dados do usuário"));

            var response = new LoginResponse(
                token.Token,
                token.Expiration,
                userDto
            );

            return Success(response);
        }

        private async Task<(string Token, DateTime Expiration)> GenerateJwtToken(User user, string? role)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? ""),
            new(ClaimTypes.Name, user.FullName ?? user.Email ?? ""),
            new("UserId", user.Id),
            new("Email", user.Email ?? "")
        };

            if (!string.IsNullOrEmpty(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:ExpireMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenString, expiration);
        }
    }
}
