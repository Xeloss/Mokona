namespace Mokona.FrontEnd.Utils
{
    using ModelValidator;
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Mokona.Core.Exceptions;
    using Mokona.FrontEnd.Models;
    using Mokona.Utils.Extensions;

    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                if (filterContext.Exception is ObjectValidationException)
                    this.HandelObjectValidationException(filterContext);

                else if (filterContext.Exception is ApplicationException)
                    this.HandleApplicationException(filterContext);

                else if (filterContext.Exception is UnauthorizedException)
                    this.HandleUnauthorizedException(filterContext);

                else
                    this.HandleException(filterContext);

            }
        }

        private void HandelObjectValidationException(ExceptionContext filterContext)
        {
            var result = new ContentResult()
            {
                Content = new ObjectValidationExceptionResponse(filterContext.Exception).ToJson(),
                ContentType = "application/json"
            };

            filterContext.Result = result;
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        private void HandleUnauthorizedException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception as UnauthorizedException;

            var jsonResult = new JsonResult()
            {
                Data = new ExceptionViewModel(exception),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            filterContext.Result = jsonResult;
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        private void HandleApplicationException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception as ApplicationException;

            var jsonResult = new JsonResult()
            {
                Data = new ExceptionViewModel(exception),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            filterContext.Result = jsonResult;
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        private void HandleException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;

            if (new HttpException(null, exception).GetHttpCode() == 500)
            {
                var result = new ViewResult();

                var messageModel = new ErrorMessage
                {
                    ShowTechnicalErrorDetails = true,
                    StackTrace = string.Concat("<b>", exception.Message, "</b><br/>", exception.StackTrace),
                    Exception = exception,
                };

                result.ViewData = new ViewDataDictionary<ErrorMessage>(messageModel);
                result.ViewName = "Error";

                filterContext.Result = result;
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
        }
    }
}
