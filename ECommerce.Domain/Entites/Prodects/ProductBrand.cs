using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entites.Prodects
{
    public  class ProductBrand:BaseEntity<int>
    {
       
        public string Name { get; set; } = default!;

    }
}
