﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Order.Models
{
    public class OrderToCreateDto
    {
        public required string BasketId { get; set; }

        public int DeliveryMethodId { get; set; }


        #region Address
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Street { get; set; }

        public required string City { get; set; }
        public required string Country { get; set; }

        #endregion




    }
}