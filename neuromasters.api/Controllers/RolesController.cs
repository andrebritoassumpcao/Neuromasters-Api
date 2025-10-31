using Microsoft.AspNetCore.Mvc;
using neuromasters.api.Models;
using neuromasters.borders.Dtos.Roles;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Roles;
using System.Net;

namespace neuromasters.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IActionResultConverter actionResultConverter) : ControllerBase
{
    private readonly IActionResultConverter _actionResultConverter = actionResultConverter;

    [HttpPost("roles")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UseCaseResponse<RoleDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<RoleDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<RoleDto>))]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request, [FromServices] ICreateRoleUseCase handler)
        => _actionResultConverter.Convert(await handler.Execute(request));

    [HttpGet("{userId}/user-role")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserRoleDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<UserRoleDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<UserRoleDto>))]
    public async Task<IActionResult> GetUserRole([FromRoute] string userId, [FromServices] IGetUserRolesUseCase handler)
    {
        var request = new GetUserRolesRequest(userId);

        return _actionResultConverter.Convert(await handler.Execute(request));
    }

    [HttpPost("assign-role")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UseCaseResponse<RoleAssignmentDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<RoleAssignmentDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<RoleAssignmentDto>))]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request, [FromServices] IAssignRoleUseCase handler)
    => _actionResultConverter.Convert(await handler.Execute(request));

    [HttpDelete("remove-role")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserRolesDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UserRolesDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UserRolesDto))]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request, [FromServices] IRemoveRoleUseCase handler)
    => _actionResultConverter.Convert(await handler.Execute(request));

    [HttpGet("roles")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UseCaseResponse<RolesListDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<RolesListDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<RolesListDto>))]
    public async Task<IActionResult> ListRoles([FromServices] IListRolesUseCase handler)
    => _actionResultConverter.Convert(await handler.Execute());


}
