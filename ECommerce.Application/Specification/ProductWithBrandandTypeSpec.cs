using ECommerce.Application.Commen;
using ECommerce.Domain.Entites.Prodects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Specification
{
    public class ProductWithBrandandTypeSpec:BaseSpeification<Product,int>
    {
        public ProductWithBrandandTypeSpec(ProductQueryParams queryParams):base(P=>(queryParams.BrandId == null || P.BrandId == queryParams.BrandId) && (queryParams.TypeId == null || P.TypeId == queryParams.TypeId)&&(queryParams.SearchValue == null || P.Name.Contains(queryParams.SearchValue)))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (queryParams.Sort)
            {
                case ProductSortingOption.NameAsc:
                    AddorderBy(p => p.Name);
                    break;
                case ProductSortingOption.NameDesc:
                    AddorderByDescending(p => p.Name);
                    break;
                case ProductSortingOption.PriceAsc:
                    AddorderBy(p => p.Price);
                    break;
                case ProductSortingOption.PriceDesc:
                    AddorderByDescending(p => p.Price);
                    break;
                default:
                    AddorderBy(p => p.Id);
                    break;
            }
            ApplyPagination((queryParams.PageIndex - 1) * queryParams.PageSize, queryParams.PageSize);
        }
        public ProductWithBrandandTypeSpec( int id):base(X=>X.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
