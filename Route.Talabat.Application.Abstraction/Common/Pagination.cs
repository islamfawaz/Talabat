using Route.Talabat.Application.Abstraction.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Common
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize, IEnumerable<T> data,int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Count { get; set; }

        public required IEnumerable<T> Data { get; set; }





    }
}
