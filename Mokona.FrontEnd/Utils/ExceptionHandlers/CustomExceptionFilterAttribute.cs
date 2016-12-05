namespace Mokona.FrontEnd.Utils
{
    using ModelValidator;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Filters;
    using Mokona.Core.Exceptions;
    using Mokona.FrontEnd.Models;
    using Mokona.FrontEnd.WebApiExtensions;

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ObjectValidationException)
            {
                var statusCode = this.ReplaceStatusCodeIfJsonp(context, HttpStatusCode.BadRequest);
                context.Response = context.Request.CreateResponse<ObjectValidationExceptionResponse>(statusCode, new ObjectValidationExceptionResponse(context.Exception), GlobalConfiguration.Configuration);
            }
            else if (context.Exception is BusinessException)
            {
                var statusCode = this.ReplaceStatusCodeIfJsonp(context, HttpStatusCode.InternalServerError);
                context.Response = context.Request.CreateResponse<ExceptionViewModel>(statusCode, new ExceptionViewModel(context.Exception), GlobalConfiguration.Configuration);
            }
            else if (context.Exception is HttpApplicationException)
            {
                var ex = (HttpApplicationException)context.Exception;
                var statusCode = this.ReplaceStatusCodeIfJsonp(context, ex.StatusCode);
                context.Response = context.Request.CreateResponse<ExceptionViewModel>(statusCode, new ExceptionViewModel(context.Exception), GlobalConfiguration.Configuration);
            }
            else
            {
                // TODO: Loguear en el lugar correspondiente.
                //Esto no se esta logeando en ningunn lado :(
                var statusCode = this.ReplaceStatusCodeIfJsonp(context, HttpStatusCode.InternalServerError);

                //string errorMessage = string.Format("An unexpected error has ocurred. Please contact the system administrator if the error persist ({0}).", DateTime.Now);
                string errorMessage = context.Exception.GetFullExceptionMessage();

                context.Response = context.Request.CreateResponse<ExceptionViewModel>(statusCode, new ExceptionViewModel(errorMessage), GlobalConfiguration.Configuration);
            }
        }

        private HttpStatusCode ReplaceStatusCodeIfJsonp(HttpActionExecutedContext context, HttpStatusCode statusCode)
        {
            if (context.Request == null || context.Request.Method != HttpMethod.Get)
                return statusCode;

            var query = context.Request.RequestUri.ParseQueryString();
            var callback = query[JsonpFormatter.DEFAULT_CALLBACK];

            return string.IsNullOrEmpty(callback)
                 ? statusCode
                 : HttpStatusCode.OK; //Si es un jsonp, tenemos que devolver 200 para que el browser acepte el script
        }
    }
}
