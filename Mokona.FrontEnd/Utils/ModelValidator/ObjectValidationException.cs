namespace Mokona.FrontEnd.Utils.ModelValidator
{
    using Core.Exceptions;
    using FluentValidation.Results;
    
    public class ObjectValidationException : BusinessException
    {
        public object InvalidObject { get; set; }

        public ValidationResult ValidationResults { get; private set; }

        public ObjectValidationException(object invalidObject, ValidationResult validationResults)
            : base("Validation errors were found")
        {
            this.ValidationResults = validationResults;
            this.InvalidObject = invalidObject;
        }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
