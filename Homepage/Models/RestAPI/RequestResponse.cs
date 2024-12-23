using System.Net;

namespace Homepage.Models.RestAPI
{
    public class RequestResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
