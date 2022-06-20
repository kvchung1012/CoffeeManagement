using System;
using System.Collections;
using System.Net;
using System.Runtime.Serialization;

namespace Coffee.Core
{
    [Serializable]
    public class UserFriendlyException : Exception
    {
        public const HttpStatusCode DefaultStatus = HttpStatusCode.BadRequest;
        private IDictionary _Data { get; set; }
        public override IDictionary Data { get => _Data; }
        public object ErrorData { get; set; }

        public UserFriendlyException()
        {
            Status = DefaultStatus;
            Detail = "";
        }

        public UserFriendlyException(HttpStatusCode status)
        {
            Status = status;
        }

        public UserFriendlyException(HttpStatusCode status, string message) : base(message)
        {
            Status = status;
            Detail = message;
        }

        public UserFriendlyException(HttpStatusCode status, string message, IDictionary data)
        {
            Status = status;
            Detail = message;
            _Data = Data;
        }

        public UserFriendlyException(object data, string message = null) : base(message)
        {
            ErrorData = data;
        }

        public UserFriendlyException(string message) : base(message)
        {
            Status = DefaultStatus;
        }

        public HttpStatusCode Status { get; set; }

        public string Detail { get; set; }

        // Without this constructor, deserialization will fail
        protected UserFriendlyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
