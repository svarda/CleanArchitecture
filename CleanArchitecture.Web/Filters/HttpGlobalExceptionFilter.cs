using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Web.InfrastructureServices.ActionResults;

namespace Web.InfrastructureServices.Filters {
    public class HttpGlobalExceptionFilter : IExceptionFilter {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger) {
            _environment = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context) {
            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);
            
            var json = new JsonErrorResponse {
                Messages = new[] { "An error ocurred." }
            };
            if (_environment.IsDevelopment()) {
                json.DeveloperMessage = context.Exception;
            }
            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }
    }
}
