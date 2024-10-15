using Route.Talabat.Controllers.Errors;
using Route.Talabat.Controllers.Exception;
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
                ApiResponse response;
                switch (ex)
                {
                
                    case NotfoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        context.Response.ContentType = "application/json";
                        response = new ApiExceptionResponse(404, ex.Message);
                        await context.Response.WriteAsync(response.ToString());

                        break;

                    default:
                        if (_environment.IsDevelopment())
                        {
                            _logger.LogError(ex, ex.Message);
                            response = new ApiExceptionResponse(500, ex.Message, ex.StackTrace?.ToString());

                        }

                        else
                        {
                            response = new ApiExceptionResponse(500);
                        }


                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(response.ToString());

                        break;
                }

      
            }

        }
    }
}
