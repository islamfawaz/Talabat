using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Controllers.Errors
{
    public class ApiValidationResponse :ApiResponse
    {
        public required IEnumerable<object> Errors { get; set; }

        public ApiValidationResponse(string ? message=null) :base(400,message)
        {
            
        }


    }
}
