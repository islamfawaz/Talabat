using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Controllers.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }

        public string ? ErrorMessage { get; set; }


        public ApiResponse(int statusCode ,string ? erroMessage=null)
        {
            StatusCode = statusCode;
            ErrorMessage = erroMessage ?? GetDefualMessageForStatusCode(statusCode);
        }

        private string? GetDefualMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request, you have made",
                401 => "Authorized, you aren't ",
                404 => "Resource not found",
                500 => "Error  are path to dark side.error lead to anger .Anger lead to hate",
                _ => null
            };
        }

        public override string ToString()
        => JsonSerializer.Serialize(this, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

    }
}
