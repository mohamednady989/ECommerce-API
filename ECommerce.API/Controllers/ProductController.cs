using ECommerce.API.Attribute;
using ECommerce.Application.Commen;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    // Automatic Valedation From data 
    public class ProductController : APIBaseController
    {
        private readonly IproductService productService;

        public ProductController(IproductService productService )
        {
            this.productService = productService;
        }
        #region GetAll Products

        [HttpGet]
        [RedisCach(90)]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery] ProductQueryParams queryParams, CancellationToken ct = default)
        {
            var result = await productService.GetAllProductsAsync(queryParams, ct);
            return ToActionResult(result);
        }
        #endregion

        #region Get Product By Id
        [Authorize]
        [HttpGet("{id}")]
         public async Task<ActionResult<ProductDTO>>GetProduct(int id,CancellationToken ct)
        {
          var result=await productService.GetProductByIdAsync(id ,ct);
            return ToActionResult(result);

        }



        #endregion

        #region Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDTO>>> GetAllTypes(CancellationToken ct=default)
        {
            return ToActionResult(await productService.GetAllTypesAsync(ct));
        }
        #endregion

        #region Get All Brandes
        [HttpGet("brandes")]
        public async Task<ActionResult<IReadOnlyList<BrandDTO>>> GetAllBrandes(CancellationToken ct = default)
        {
            return ToActionResult(await productService.GetAllBrandsAsync(ct));
        }
        #endregion



    }
}
