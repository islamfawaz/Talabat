using Route.Talabat.Controllers.Errors;
using Route.Talabat.Core.Application.Exception;
using System.Net;

namespace Route.Talabat.APIs.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        // Middleware constructor to inject required dependencies
        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        // Middleware invoke method
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Forward the request to the next middleware
                await _next(context);

                if (context.Response.StatusCode == 404)
                {
                    var response = new ApiResponse(404, $"The requested endpoint: {context.Request.Path} is not found");
                    await context.Response.WriteAsync(response.ToString());
                }
            }
            catch (Exception ex)
            {
                 if (_environment.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message);   
                }
                else
                {
                    _logger.LogError("An unhandled exception occurred: {Message}", ex.Message);
                }

                await HandleExceptionAsync(context, ex);   
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ApiResponse response;

            switch (ex)
            {
                case NotfoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new ApiExceptionResponse(404, ex.Message);
                    break;

                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ApiExceptionResponse(400, ex.Message);
                    break;

                case UnAuthorizedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new ApiExceptionResponse(401, ex.Message);
                    break;

                default:
                    response = _environment.IsDevelopment()
                        ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                        : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                    context.Response.StatusCode = 500;
                    break;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(response.ToString());
        }
    }
}
