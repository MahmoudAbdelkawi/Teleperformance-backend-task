using System.Net;

namespace Authentication_CRUD_Operation.Globals
{
    public class BaseResponse<TData>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public TData? Data { get; set; }
    }
}
