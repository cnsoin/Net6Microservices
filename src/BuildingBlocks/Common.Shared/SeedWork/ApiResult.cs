namespace Common.Shared.SeedWork
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }

        public string? Message { get; set; }

        public T? ResultObj { get; set; }
    }
}