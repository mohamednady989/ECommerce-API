using ECommerce.Application.Commen;
using ECommerce.Domain.Entites.Prodects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Specification
{
    public  class ProductCountSpecification :BaseSpeification<Product,int >
    {

        public ProductCountSpecification(ProductQueryParams queryParams) : base(P => (queryParams.BrandId == null || P.BrandId == queryParams.BrandId) && (queryParams.TypeId == null || P.TypeId == queryParams.TypeId) && (queryParams.SearchValue == null || P.Name.Contains(queryParams.SearchValue)))
        {
            
        }

    }
}
