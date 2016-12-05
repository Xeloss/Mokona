namespace Mokona.FrontEnd.Models
{
    using Mokona.Core.Exceptions;
    using System;

    public class ExceptionViewModel
    {
        public ExceptionViewModel(Exception ex)
        {
            this.Message = ex.Message;
            this.StackTrace = ex.StackTrace;
            this.StatusCode = 500;

            if (ex.InnerException != null)
            {
                this.InnerException = new ExceptionViewModel(ex.InnerException);
            }
        }

        public ExceptionViewModel(HttpApplicationException ex)
            : this((Exception)ex)
        {
            this.StatusCode = (int)ex.StatusCode;
        }

        public ExceptionViewModel(string message)
        {
            this.Message = message;
            this.StackTrace = string.Empty;
            this.InnerException = null;
            this.StatusCode = 500;
        }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public int StatusCode { get; set; }

        public ExceptionViewModel InnerException { get; set; }
    }
}
