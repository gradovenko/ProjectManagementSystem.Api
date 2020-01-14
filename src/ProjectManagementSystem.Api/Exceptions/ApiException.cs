using System;
using System.Collections.Generic;
using System.Net;

namespace ProjectManagementSystem.Api.Exceptions
{
    public class ApiException : Exception
    {
        private readonly HttpStatusCode _httpStatusCode;

        public int HttpStatusCode => (int) _httpStatusCode;

        public string Description { get; }

        public IDictionary<string, object> Fields { get; }

        public ApiException(HttpStatusCode httpStatusCode, string code, string message)
            : this(httpStatusCode, code, message, null)
        {
            _httpStatusCode = httpStatusCode;
        }

        public ApiException(HttpStatusCode httpStatusCode, string message, string description,
            Exception exception = null)
            : base(message, exception)
        {
            _httpStatusCode = httpStatusCode;
            Description = description;
        }

        public ApiException(HttpStatusCode httpStatusCode, string message, string description,
            IDictionary<string, object> fields, Exception exception = null)
            : base(message, exception)
        {
            _httpStatusCode = httpStatusCode;
            Description = description;
            Fields = fields;
        }
    }
}