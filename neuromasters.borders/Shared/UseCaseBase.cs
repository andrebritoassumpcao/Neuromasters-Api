using Microsoft.Extensions.Logging;
using System.Diagnostics;
using FluentValidation;
namespace neuromasters.borders.Shared;

public abstract class UseCaseBase<TResponse>(ILogger<UseCaseBase<TResponse>> _logger, ErrorMessage errorMessage)
{
    private const int WARNING_EXECUTION_TIME = 1000;
    private const int ERROR_EXECUTION_TIME = 8000;
    protected ErrorMessage ErrorMessage { get; set; } = errorMessage;

    protected async Task<UseCaseResponse<TResponse>> SafeExecute(Func<Task<UseCaseResponse<TResponse>>> func)
    {
        var stopwatch = StartExecution();
        try
        {
            var result = await func.Invoke();
            StopExecution(stopwatch);
            return result;
        }
        catch (ValidationException ex)
        {
            StopExecution(stopwatch);
            _logger.LogWarning(ex, "UseCase {@UseCaseName} execution validation fail ex: {@ex}", GetType().Name, ex);
            return RequestValidationError(ex);
        }
        catch (Exception ex)
        {
            StopExecution(stopwatch);
            _logger.LogError(ex, "UseCase {@UseCaseName} execution fail ex: {@ex}", GetType().Name, ex);
            return InternalServerError(ex.Message,
                [new ErrorMessage("0", $"Unexpected error during the {GetType().Name} execution")]);
        }
    }

    private static Stopwatch StartExecution()
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        return stopwatch;
    }

    private void StopExecution(Stopwatch stopwatch)
    {
        stopwatch.Stop();
        if (stopwatch.ElapsedMilliseconds > WARNING_EXECUTION_TIME ||
            stopwatch.ElapsedMilliseconds > ERROR_EXECUTION_TIME)
        {
            _logger.LogWarning("UseCase {@UseCaseName} took {@ElapsedMilliseconds}ms to execute",
                GetType().Name, stopwatch.ElapsedMilliseconds);
        }
    }

    protected UseCaseResponse<TResponse> Success(TResponse result) =>
        new UseCaseResponse<TResponse>().SetSuccess(result);

    protected UseCaseResponse<TResponse> Persisted(TResponse result, string resultId) =>
        new UseCaseResponse<TResponse>().SetPersisted(result, resultId);

    protected UseCaseResponse<TResponse> Persisted(TResponse result, Guid resultId) =>
        new UseCaseResponse<TResponse>().SetPersisted(result, resultId.ToString());

    protected UseCaseResponse<TResponse> Processed(TResponse result, string resultId) =>
        new UseCaseResponse<TResponse>().SetProcessed(result, resultId);

    protected UseCaseResponse<TResponse> Processed(TResponse result, Guid resultId) =>
        new UseCaseResponse<TResponse>().SetProcessed(result, resultId.ToString());

    protected UseCaseResponse<TResponse> InternalServerError(string erroMessage,
        IEnumerable<ErrorMessage> errors = null)
        => new UseCaseResponse<TResponse>().SetInternalServerError(erroMessage, errors);

    protected UseCaseResponse<TResponse> Unavailable(TResponse result)
        => new UseCaseResponse<TResponse>().SetUnvailable(result);

    protected UseCaseResponse<TResponse> RequestValidationError(string errorMessage,
        IEnumerable<ErrorMessage> errors = null)
        => new UseCaseResponse<TResponse>().SetRequestValidationError(errorMessage, errors);

    protected UseCaseResponse<TResponse> RequestValidationError(ErrorMessage errorMessage)
        => new UseCaseResponse<TResponse>().SetRequestValidationError(errorMessage.Message,
            new ErrorMessage[] { errorMessage });

    protected UseCaseResponse<TResponse> RequestValidationError(ValidationException ex)
        => new UseCaseResponse<TResponse>().SetRequestValidationError(ex);

    protected UseCaseResponse<TResponse> BadRequest(ErrorMessage errorMessage) =>
        new UseCaseResponse<TResponse>().SetBadRequest(errorMessage);

    protected UseCaseResponse<TResponse> NotFound(ErrorMessage errorMessage) =>
        new UseCaseResponse<TResponse>().SetNotFound(errorMessage);

    protected UseCaseResponse<TResponse> SetStatus(UseCaseResponseKind status, string errorMessage = null,
        IEnumerable<ErrorMessage> errors = null)
        => new UseCaseResponse<TResponse>().SetStatus(status, errorMessage, errors);
}
