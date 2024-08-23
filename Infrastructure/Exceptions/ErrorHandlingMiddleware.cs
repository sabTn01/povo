using System.Net;
using Newtonsoft.Json;

namespace POVO.Backend.Infrastructure.Exceptions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            _logger = loggerFactory?.CreateLogger<ErrorHandlingMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task<Task> HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = (int) HttpStatusCode.InternalServerError; // 500 if unexpected

            var exceptionWrapper = new ApiExceptionWrapper()
            {
                ExceptionName = exception.GetType().FullName,
                ExceptionMessage = exception.Message
            };

#if DEBUG
            exceptionWrapper.Callstack = exception.StackTrace.Split(Environment.NewLine);
#endif

            if (exception is EntityNotFoundException) code = (int) HttpStatusCode.NotFound;
            else if (exception is AuthentificationFailedException) code = (int) HttpStatusCode.Unauthorized;
            else if (exception is PermissionFailedException) code = (int) HttpStatusCode.Forbidden;
            else if (exception is BadRequestException) code = (int) HttpStatusCode.BadRequest;
            else if (exception is EntityValidationErrorException errorException)
            {
                code = 422;
                exceptionWrapper.Errors = errorException.Errors;
            }
            else
            {
                _logger.LogError(exception, "Error", null);
            }

            var result = JsonConvert.SerializeObject(exceptionWrapper);

            if (context.Response.Headers.ContainsKey("Access-Control-Allow-Origin") == false)
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(result);
        }
    }
}