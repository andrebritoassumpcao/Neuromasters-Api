using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using neuromasters.borders.Entities;

namespace neuromasters.api.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

    /*    // DTO para cadastro
        public class RegisterRequest
        {
            public string DocumentNumber { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string FullName { get; set; } = null!;
            public string PhoneNumber { get; set; } = null!;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = new User
            {
                Id
                UserName = request.FullName,
                Email = request.Email,
                EmailConfirmed = request.Email,
                PasswordHash = request.Password,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed

                
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // opcional: atribuir Role
            // await _userManager.AddToRoleAsync(user, "Cliente");

            return Ok(new { message = "Usuário registrado com sucesso" });
        }*/
    }
}
