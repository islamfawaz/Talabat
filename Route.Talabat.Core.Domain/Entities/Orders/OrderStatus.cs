using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.OrderAggregate
{
     public enum OrderStatus
    {

        Pending=1,
        PaymentReceived=2,
        PaymentFailed=3,

    }
}
