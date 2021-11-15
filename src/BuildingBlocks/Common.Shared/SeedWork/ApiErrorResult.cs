namespace Common.Shared.SeedWork
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public List<string>? ValidationErrors { get; set; }

        public ApiErrorResult()
        {
            ValidationErrors = new List<string>();
        }

        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ApiErrorResult(List<string> validationErrors)
        {
            IsSuccessed = false;
            ValidationErrors = validationErrors;
        }
    }
}