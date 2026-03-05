namespace Travel.API.Helpers
{
    public class JsonResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic data { get; set; }
    }
    public class JsonResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T data { get; set; }
    }
}
