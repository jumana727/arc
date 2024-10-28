using Arc.Common.Enums;

namespace Arc.Common.Models
{

    public class DataModel<T>
    {

        public required T Data { get; set; }

        public HttpStatusCode Status { get; set; }

        public string Message { get; set; } = string.Empty;

    }

}