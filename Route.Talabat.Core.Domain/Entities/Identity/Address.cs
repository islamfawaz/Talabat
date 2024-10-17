using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Entities.Identity
{
    public class Address
    {
        public required int Id { get; set; }

        public required string FName { get; set; }
        public required string LName { get; set; }

        public required string Street { get; set; }

        public required string City { get; set; }

        public required string Country { get; set; }


        public int UserId { get; set; }
        public required ApplicationUser AppUser { get; set; }




    }
}
