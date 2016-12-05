namespace Mokona.FrontEnd.Utils
{
    using System;

    public static class ExceptionExtensions
    {
        public static string GetFullExceptionMessage(this Exception ex)
        {
            if (ex.InnerException != null)
                return ex.Message + ". " + ex.InnerException.GetFullExceptionMessage();

            return ex.Message;
        }
    }
}
