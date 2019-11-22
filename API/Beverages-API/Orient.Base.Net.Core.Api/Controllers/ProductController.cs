using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orient.Base.Net.Core.Api.Core.Business.Filters;
using Orient.Base.Net.Core.Api.Core.Business.Models.Products;
using Orient.Base.Net.Core.Api.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Controllers
{
    [Route("api/products")]
    [EnableCors("CorsPolicy")]
    [ValidateModel]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(ProductRequestListViewModel productRequestListViewModel)
        {
            var product = await _productService.ListProductAsync(productRequestListViewModel);
            return Ok(product);
        }

        [HttpGet("all-products")]
        public async Task<IActionResult> GetAllProducts(ProductRequestGetAllModel productRequestGetAllModel)
        {
            var product = await _productService.GetAllProductAsync(productRequestGetAllModel);
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var responseModel = await _productService.GetProductByIdAsync(id);
            if (responseModel.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(responseModel);
            }
            else
            {
                return BadRequest(new { Message = responseModel.Message });
            }
        }

        [HttpPost]
        [CustomAuthorize]
        public async Task<IActionResult> Post([FromBody] ProductManageModel productManagerModel)
        {
            var responseModel = await _productService.CreateProductAsync(productManagerModel);
            if (responseModel.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(responseModel);
            }
            else
            {
                return BadRequest(new { Message = responseModel.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductManageModel productManageModel)
        {
            var responseModel = await _productService.UpdateProductAsync(id, productManageModel);
            if (responseModel.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(responseModel);
            }
            else
            {
                return BadRequest(new { Message = responseModel.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var responseModel = await _productService.DeleteProductAsync(id);
            if (responseModel.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(responseModel);
            }
            else
            {
                return BadRequest(new { Message = responseModel.Message });
            }
        }
    }
}
