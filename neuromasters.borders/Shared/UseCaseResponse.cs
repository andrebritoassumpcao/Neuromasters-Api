using FluentValidation;

namespace neuromasters.borders.Shared
{
    public class UseCaseResponse<T>
    {
        public UseCaseResponseKind Status { get; set; }
        public string ErrorMessage { get; private set; }
        public IEnumerable<ErrorMessage> Errors { get; set; }
        public T Result { get; set; }
        public string ResultId { get; set; }

        public UseCaseResponse<T> SetSuccess(T result)
        {
            Result = result;
            return SetStatus(UseCaseResponseKind.Success);
        }

        public UseCaseResponse<T> SetPersisted(T result, string resultId)
        {
            Result = result;
            ResultId = resultId;
            return SetStatus(UseCaseResponseKind.DataPersisted);
        }

        public UseCaseResponse<T> SetProcessed(T result, string resultId)
        {
            Result = result;
            ResultId = resultId;
            return SetStatus(UseCaseResponseKind.DataAccepted);
        }

        public UseCaseResponse<T> SetInternalServerError(string errorMessage, IEnumerable<ErrorMessage> errors = null) =>
            SetStatus(UseCaseResponseKind.InternalServerError, errorMessage, errors);

        public UseCaseResponse<T> SetUnvailable(T result)
        {
            Result = result;
            Status = UseCaseResponseKind.Unavailable;
            ErrorMessage = "Unavailable";
            return this;
        }

        public UseCaseResponse<T> SetRequestValidationError(string errorMessage, IEnumerable<ErrorMessage> errors = null) =>
            SetStatus(UseCaseResponseKind.RequestValidationError, errorMessage, errors);


        public UseCaseResponse<T> SetRequestValidationError(ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new ErrorMessage(e.ErrorCode, e.ErrorMessage));
            if (!errors.Any())
            {
                errors = [new ErrorMessage("0.0.0", ex.Message)];
            }

            return SetRequestValidationError(ex.Message, errors);
        }

        public UseCaseResponse<T> SetNotFound(ErrorMessage error) =>
            SetStatus(UseCaseResponseKind.NotFound, "Dat not found", new ErrorMessage[] { error });

        public UseCaseResponse<T> SetBadRequest(ErrorMessage error) =>
            SetStatus(UseCaseResponseKind.BadRequest, "Bad Request", new ErrorMessage[] { error });

        public UseCaseResponse<T> SetStatus(UseCaseResponseKind status, string erroMessage = null,
            IEnumerable<ErrorMessage> errors = null)
        {
            Status = status;
            ErrorMessage = erroMessage;
            Errors = errors;

            return this;
        }

        public bool Success() =>
            ErrorMessage is null && Errors is null;
    }


}
