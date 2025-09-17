using Microsoft.AspNetCore.Mvc;
using neuromasters.borders.Shared;
using Serilog;
using System.Net;

namespace neuromasters.api.Models;

public interface IActionResultConverter
{
    IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSuccess = false);
}

public class ActionResultConverter : IActionResultConverter
{
    private readonly string path;

    public ActionResultConverter(IHttpContextAccessor accessor)
    {
        path = accessor.HttpContext?.Request.Path.Value;
    }
    public IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSuccess = false)
    {
        if (response is null)
        {
            return BuildError(new[] { new ErrorMessage("0", "ActionResultConverter Error") },
                UseCaseResponseKind.InternalServerError);
        }

        if (response.Success())
        {
            if (noContentIfSuccess)
            {
                return new NoContentResult();
            }

            return BuildSuccessResult(response.Result, response.ResultId, response.Status);
        }
        else if (response.Result is not null)
        {
            return BuildError(response.Result, response.Status);
        }
        else
        {
            var hasError = response.Errors is null || !response.Errors.Any();
            var errorResult = hasError ?
                new[] { new ErrorMessage("0", response.ErrorMessage ?? "Unknown error") }
                : response.Errors;

            return BuildError(errorResult, response.Status);
        }
    }

    private IActionResult BuildSuccessResult(object data, string id, UseCaseResponseKind status)
    {
        return status switch
        {
            UseCaseResponseKind.DataPersisted => new CreatedResult($"{path}/{id}/", data),
            UseCaseResponseKind.DataAccepted => new AcceptedResult($"{path}/{id}", data),
            _ => new OkObjectResult(data)
        };
    }

    private ObjectResult BuildError(object data, UseCaseResponseKind status)
    {
        var httpStatus = GetErrorHttpStatusCode(status);
        if (httpStatus == HttpStatusCode.InternalServerError)
        {
            Log.Error("[ERROR] {@Path} ({@Data})", path, data);
        }
        else if (data is ErrorMessage || data is IEnumerable<ErrorMessage> errors && errors.Any())
        {
            Log.Information("[ProcessDetails] {@Path} ({@Data})", path, data);
        }

        return new ObjectResult(data)
        {
            StatusCode = (int)httpStatus
        };
    }

    private HttpStatusCode GetErrorHttpStatusCode(UseCaseResponseKind status)
    {
        return status switch
        {
            UseCaseResponseKind.RequestValidationError or UseCaseResponseKind.BadRequest => HttpStatusCode.BadRequest,
            UseCaseResponseKind.Unauthorized => HttpStatusCode.Unauthorized,
            UseCaseResponseKind.Forbidden => HttpStatusCode.Forbidden,
            UseCaseResponseKind.NotFound or UseCaseResponseKind.ForeignKeyViolationError => HttpStatusCode.NotFound,
            UseCaseResponseKind.UniqueViolationError => HttpStatusCode.Conflict,
            UseCaseResponseKind.Unavailable => HttpStatusCode.ServiceUnavailable,
            UseCaseResponseKind.UnprocssableEntity => HttpStatusCode.UnprocessableEntity,
            _ => HttpStatusCode.InternalServerError
        };
    }
}
