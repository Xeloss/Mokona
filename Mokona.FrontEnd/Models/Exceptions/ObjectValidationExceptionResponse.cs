namespace Mokona.FrontEnd.Models
{
    using FluentValidation.Results;
    using System;
    using Utils.ModelValidator;

    public class ObjectValidationExceptionResponse
    {
        private ObjectValidationExceptionResponse()
        {
            this.Message = string.Empty;
            this.ValidationResults = new ValidationResult();
        }

        public ObjectValidationExceptionResponse(Exception ex)
        {
            this.Message = ex.Message;

            var resourceValidationEx = ex as ObjectValidationException;
            if (resourceValidationEx != null)
                this.ValidationResults = resourceValidationEx.ValidationResults;
        }

        public string Message { get; set; }

        public ValidationResult ValidationResults { get; set; }
    }
}
