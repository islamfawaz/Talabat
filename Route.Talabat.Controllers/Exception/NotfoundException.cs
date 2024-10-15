using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Controllers.Exception
{
    public class NotfoundException :ApplicationException
    {
        public NotfoundException()
            :base("Not Found")
        {
            

        }
    }
}
