namespace Mokona.FrontEnd.Models
{
    using System;

    public class ErrorMessage
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Description { get; set; }

        public Exception Exception { get; set; }

        public bool ShowTechnicalErrorDetails { get; set; }

        public string StackTrace { get; set; }
    }
}
