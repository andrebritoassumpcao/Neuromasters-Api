using Microsoft.AspNetCore.Mvc;
using neuromasters.api.Models;
using neuromasters.borders.Dtos;
using neuromasters.borders.Dtos.Auth;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Auth;
using System.Net;

namespace neuromasters.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IActionResultConverter actionResultConverter) : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter = actionResultConverter;

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UseCaseResponse<UserDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<UserDto>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<UserDto>))]
        public async Task<IActionResult> CreateUser([FromBody] RegisterRequest request, [FromServices] IRegisterUserUseCase handler)
                => _actionResultConverter.Convert(await handler.Execute(request));

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UseCaseResponse<LoginResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<LoginResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<LoginResponse>))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, [FromServices] ILoginUseCase handler)
            => _actionResultConverter.Convert(await handler.Execute(request));

    }
}
