using System;
using System.Runtime.Serialization;

namespace TicketEagle.Controllers
{
    [Serializable]
    internal class HttpResponseException : Exception
    {
        private object unauthorized;

        public HttpResponseException()
        {
        }

        public HttpResponseException(object unauthorized)
        {
            this.unauthorized = unauthorized;
        }

        public HttpResponseException(string message) : base(message)
        {
        }

        public HttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string ReasonPhrase { get; internal set; }
    }
}