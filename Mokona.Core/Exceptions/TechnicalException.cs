namespace Mokona.Core.Exceptions
{
    using System;

    public class TechnicalException : ApplicationException
    {
        public TechnicalException(string ErrorMessage)
            : base(ErrorMessage)
        {
        }

        public TechnicalException(string ErrorMessage, Exception innerException)
            : base(ErrorMessage, innerException)
        {
        }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
