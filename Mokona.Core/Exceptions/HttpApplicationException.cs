namespace Mokona.Core.Exceptions
{
    using System;
    using System.Net;

    public abstract class HttpApplicationException : ApplicationException
    {
        public HttpStatusCode StatusCode { get; private set; }

        public HttpApplicationException(HttpStatusCode statusCode)
            : base(statusCode.ToString())
        {
            this.StatusCode = statusCode;
        }

        public override string Message
        {
            get
            {
                return base.Message;
            }
        }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
