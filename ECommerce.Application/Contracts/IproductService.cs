using ECommerce.Application.Commen;
using ECommerce.Application.DTO.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface IproductService
    {
        Task <Result<PaginatedResult<ProductDTO>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default);
        Task<Result<IReadOnlyList<BrandDTO>>> GetAllBrandsAsync(CancellationToken ct = default);
        Task <Result<IReadOnlyList<TypeDTO>>> GetAllTypesAsync(CancellationToken ct = default);
        Task<Result<ProductDTO>> GetProductByIdAsync(int id, CancellationToken ct = default);
    }
}
