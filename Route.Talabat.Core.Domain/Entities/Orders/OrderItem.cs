﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.OrderAggregate
{
    public class OrderItem:BaseAuditableEntity<int>
    {

        //  public required ProductItemOrder Product { get; set; }
        #region Product
        public int ProductId { get; set; }
        public required string ProductName { get; set; }

        public required string PictureUrl { get; set; } 
        #endregion
        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }
}
