namespace Mokona.Core.Exceptions
{
    using System;

    public class BusinessException : ApplicationException
    {
        public BusinessException(string ErrorMessage)
            : base(ErrorMessage)
        {
        }

        public BusinessException(string ErrorMessage, Exception innerException)
            : base(ErrorMessage, innerException)
        {
        }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
