using Azure;
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

        public CustomExceptionHandlerMiddleware(RequestDelegate next ,ILogger<CustomExceptionHandlerMiddleware> logger ,IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //Logic Executed with Request
                await _next(context);
                //Logic Executed with Response

                if (context.Response.StatusCode == 404)
                {
                    var response = new ApiResponse(404, $"The request endPoint :{context.Request.Path} is not found");
                    await context.Response.WriteAsync(response.ToString());

                }
            }
            catch (Exception ex)
            {

                #region LogginTODO
                if (_environment.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message);

                }

                else
                {
                }


                #endregion

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
                    context.Response.ContentType = "application/json";
                    response = new ApiExceptionResponse(404, ex.Message);
                    await context.Response.WriteAsync(response.ToString());

                    break;


                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    response = new ApiExceptionResponse(400, ex.Message);
                    await context.Response.WriteAsync(response.ToString());

                    break;

                     default:

                    response = _environment.IsDevelopment() ?
                        new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                        :
                         new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(response.ToString());

                    break;
            }
        }
    }
}
