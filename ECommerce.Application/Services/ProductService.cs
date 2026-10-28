using AutoMapper;
using ECommerce.Application.Commen;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.Products;
using ECommerce.Application.Specification;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entites.Prodects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public  class ProductService:IproductService
    {
        private IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<Result<PaginatedResult<ProductDTO>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default)
        {
            var spec= new ProductWithBrandandTypeSpec(queryParams);
            var products = await UnitOfWork.GetRepostiory<Product, int>().GetAllAsync();
            var countSpce=new ProductCountSpecification(queryParams);
            var countofAllProducts = await UnitOfWork.GetRepostiory<Product, int>().CountAsync(countSpce);
            var data = Mapper.Map<IReadOnlyList<ProductDTO>>(products);
            var result = new PaginatedResult<ProductDTO>(queryParams.PageIndex, queryParams.PageSize, countofAllProducts, data);
            var brands = await UnitOfWork.GetRepostiory<ProductBrand, int>().GetAllAsync(ct);
            var types = await UnitOfWork.GetRepostiory<ProductType, int>().GetAllAsync(ct);

            var brandDict = brands.ToDictionary(b => b.Id, b => b.Name);
            var typeDict = types.ToDictionary(t => t.Id, t => t.Name);

            var list = new List<ProductDTO>(products.Count);
            foreach (var p in products)
            {
                var dto = Mapper.Map<ProductDTO>(p);
                dto.ProductBrand = brandDict.TryGetValue(p.BrandId, out var bn) ? bn : string.Empty;
                dto.ProductType = typeDict.TryGetValue(p.TypeId, out var tn) ? tn : string.Empty;
                list.Add(dto);
            }

            return Result<PaginatedResult<ProductDTO>>.Ok(result);

        }

        public async Task<Result<IReadOnlyList<BrandDTO>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var products = await UnitOfWork.GetRepostiory<ProductBrand, int>().GetAllAsync(ct);
            return Result<IReadOnlyList<BrandDTO>>.Ok(Mapper.Map<IReadOnlyList<BrandDTO>>(products));
        }

        public async Task<Result<IReadOnlyList<TypeDTO>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var type = Mapper.Map<IReadOnlyList<TypeDTO>>(await UnitOfWork.GetRepostiory<ProductType, int>().GetAllAsync(ct));
            return Result<IReadOnlyList<TypeDTO>>.Ok(type);

        }

        public async Task<Result<ProductDTO>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var spec=new ProductWithBrandandTypeSpec(id);
            var product = await UnitOfWork.GetRepostiory<Product, int>().GetByIdAsync(spec, ct);
            if (product is null)
                return Result<ProductDTO>.Fail(Error.NotFound("Product.NotFound ", $"Product Wiht Id {id} Not found"));
            return Result<ProductDTO>.Ok(Mapper.Map<ProductDTO>(product));
        }

      

        // Interface implementation is provided by the public GetProductByIdAsync
    }
}
