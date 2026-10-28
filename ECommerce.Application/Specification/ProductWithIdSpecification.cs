using ECommerce.Domain.Entites.Prodects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Specification
{
    public  class ProductWithIdSpecification:BaseSpeification<Product ,int >
    {
        public ProductWithIdSpecification(HashSet<int>productIds):base(P=>productIds.Contains(P.Id))
        {
            
        }
    }
}
