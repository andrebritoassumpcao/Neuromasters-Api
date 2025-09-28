using Microsoft.AspNetCore.Mvc;
using neuromasters.api.Models;
using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Dtos.Questionnaires.Forms;
using neuromasters.borders.Shared;
using neuromasters.borders.UseCases.Questionnaires.Form;
using neuromasters.borders.UseCases.Questionnaires.SkillGroups;
using System.Net;
using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using neuromasters.borders.UseCases.Questionnaires.DefaultAswers;

namespace neuromasters.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionnaireController(IActionResultConverter actionResultConverter) : ControllerBase
{
    private readonly IActionResultConverter _actionResultConverter = actionResultConverter;

    #region SkillGroups
    [HttpPost("create-groups")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SkillGroupDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    public async Task<IActionResult> CreateSkillGroup([FromBody] CreateSkillGroupRequest request, [FromServices] ICreateSkillGroupUseCase handler)
            => _actionResultConverter.Convert(await handler.Execute(request));

    [HttpPut("update-groups")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SkillGroupDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    public async Task<IActionResult> UpdateSkillGroup([FromBody] UpdateSkillGroupRequest request, [FromServices] IUpdateSkillGroupUseCase handler)
         => _actionResultConverter.Convert(await handler.Execute(request));

    [HttpGet("list-groups")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<SkillGroupDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<IEnumerable<SkillGroupDto>>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<IEnumerable<SkillGroupDto>>))]
    public async Task<IActionResult> ListSkillGroups([FromServices] IListSkillGroupsUseCase handler)
        => _actionResultConverter.Convert(await handler.Execute());

    [HttpGet("get-group/{code}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SkillGroupDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<SkillGroupDto>))]
    public async Task<IActionResult> GetSkillGroup([FromRoute] string code, [FromServices] IGetSkillGroupUseCase handler)
    {
        var request = new GetSkillGroupRequest(code);

        return _actionResultConverter.Convert(await handler.Execute(request));
    }

    [HttpDelete("delete-group/{code}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<bool>))]
    public async Task<IActionResult> DeleteSkillGroup([FromRoute] string code, [FromServices] IDeleteSkillGroupUseCase handler)
    {
        var request = new GetSkillGroupRequest(code);

        return _actionResultConverter.Convert(await handler.Execute(request));
    }
    #endregion

    [HttpPost("create-form")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuestionnaireDetailDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<QuestionnaireDetailDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<QuestionnaireDetailDto>))]
    public async Task<IActionResult> CreateForm([FromBody] CreateQuestionnaireRequest request, [FromServices] ICreateQuestionnaireUseCase handler)
        => _actionResultConverter.Convert(await handler.Execute(request));

    [HttpPut("update-form")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuestionnaireDetailDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<QuestionnaireDetailDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<QuestionnaireDetailDto>))]
    public async Task<IActionResult> UpdateForm([FromBody] UpdateQuestionnaireRequest request, [FromServices] IUpdateQuestionnaireUseCase handler)
    => _actionResultConverter.Convert(await handler.Execute(request));

    [HttpGet("list-forms")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<QuestionnaireListDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<IEnumerable<QuestionnaireListDto>>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<IEnumerable<QuestionnaireListDto>>))]
    public async Task<IActionResult> ListForms([FromServices] IListQuestionnairesUseCase handler)
    => _actionResultConverter.Convert(await handler.Execute());

    [HttpGet("get-form/{code}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuestionnaireDetailDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<QuestionnaireDetailDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<QuestionnaireDetailDto>))]
    public async Task<IActionResult> GetForm([FromRoute] int code, [FromServices] IGetQuestionnaireUseCase handler)
    {
        var request = new GetQuestionnaireRequest(code);

        return _actionResultConverter.Convert(await handler.Execute(request));
    }

    [HttpDelete("delete-form/{code}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<bool>))]
    public async Task<IActionResult> DeleteForm([FromRoute] int code, [FromServices] IDeleteQuestionnaireUseCase handler)
    {
        var request = new GetQuestionnaireRequest(code);

        return _actionResultConverter.Convert(await handler.Execute(request));
    }

    //DefaultAnswers

    [HttpPost("create-default-answer")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DefaultAnswerDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<DefaultAnswerDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<DefaultAnswerDto>))]
    public async Task<IActionResult> CreateDefaultAnswer([FromBody] CreateDefaultAnswerRequest request, [FromServices] ICreateDefaultAnswerUseCase handler)
    => _actionResultConverter.Convert(await handler.Execute(request));

    [HttpGet("list-default-answers/{questionnaireId}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<DefaultAnswerDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<IEnumerable<DefaultAnswerDto>>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<IEnumerable<DefaultAnswerDto>>))]
    public async Task<IActionResult> ListDefaultAnswers([FromRoute] int questionnaireId, [FromServices] IListDefaultAnswerUseCase handler)
    {
        var request = new ListDefaultAnswerRequest(questionnaireId);

        return _actionResultConverter.Convert(await handler.Execute(request));
    }

    [HttpGet("get-default-answer/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DefaultAnswerDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<DefaultAnswerDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<DefaultAnswerDto>))]
    public async Task<IActionResult> GetDefaultAnswer([FromRoute] int id, [FromServices] IGetDefaultAnswerUseCase handler)
    {
        var request = new GetDefaultAnswerRequest(id);

        return _actionResultConverter.Convert(await handler.Execute(request));
    }

    [HttpDelete("delete-default-answer/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UseCaseResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(UseCaseResponse<bool>))]
    public async Task<IActionResult> DeleteDefaultAnswer([FromRoute] int id, [FromServices] IDeleteDefaultAnswerUseCase handler)
    {
        var request = new GetDefaultAnswerRequest(id);

        return _actionResultConverter.Convert(await handler.Execute(request));
    }
}