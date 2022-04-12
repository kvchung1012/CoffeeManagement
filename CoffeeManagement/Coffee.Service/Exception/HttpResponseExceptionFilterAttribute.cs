using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Exception
{
    public class HttpResponseExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<HttpResponseExceptionFilterAttribute> _logger;

        public HttpResponseExceptionFilterAttribute(ILogger<HttpResponseExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode;

            statusCode = HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(new ExceptionResultModel
            {
                Message = new[] { context.Exception.Message },
                Data = context.Exception.Data
            });
            _logger.LogError(context.Exception, context.Exception.Message);
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.ExceptionHandled = true;
        }
    }
}
