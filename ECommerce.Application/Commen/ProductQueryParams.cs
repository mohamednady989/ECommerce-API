using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Commen
{
    public  class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? SearchValue { get; set; }
        public ProductSortingOption Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        private int pageSize=DefaultPageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }


    }
}
