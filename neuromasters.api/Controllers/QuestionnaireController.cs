using Microsoft.AspNetCore.Mvc;
using neuromasters.api.Models;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.SkillGroups;
using System.Net;

namespace neuromasters.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionnaireController(IActionResultConverter actionResultConverter) : ControllerBase
{
    private readonly IActionResultConverter _actionResultConverter = actionResultConverter;

    [HttpPost("create-groups")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    public async Task<IActionResult> CreateSkillGroup([FromBody] CreateSkillGroupRequest request, [FromServices] ICreateSkillGroupUseCase handler)
            => _actionResultConverter.Convert(await handler.Execute(request));

    [HttpGet("list-groups")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UseCaseResponse<IEnumerable<SkillGroupDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<IEnumerable<SkillGroupDto>>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<IEnumerable<SkillGroupDto>>))]
    public async Task<IActionResult> ListSkillGroups([FromServices] IListSkillGroupsUseCase handler)
        => _actionResultConverter.Convert(await handler.Execute());

    [HttpGet("groups/{code}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    public async Task<IActionResult> GetUserRole([FromRoute] string code, [FromServices] IGetSkillGroupUseCase handler)
    {
        var request = new GetSkillGroupRequest(code);

        return _actionResultConverter.Convert(await handler.Execute(request));
    }
}