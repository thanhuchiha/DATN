using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orient.Base.Net.Core.Api.Core.Business.Filters;
using Orient.Base.Net.Core.Api.Core.Business.Models.Shops;
using Orient.Base.Net.Core.Api.Core.Business.Services;
using System;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Controllers
{
    [Route("api/shops")]
    [EnableCors("CorsPolicy")]
    [ValidateModel]
    public class ShopController : BaseController
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(ShopRequestListViewModel shopRequestListViewModel)
        {
            var shop = await _shopService.ListShopAsync(shopRequestListViewModel);
            return Ok(shop);
        }

        [HttpGet("all-shops")]
        public async Task<IActionResult> GetAllShops(ShopRequestGetAllModel shopRequestGetAllModel)
        {
            var shop = await _shopService.GetAllShopAsync(shopRequestGetAllModel);
            return Ok(shop);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopById(Guid id)
        {
            var responseModel = await _shopService.GetShopByIdAsync(id);
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
        public async Task<IActionResult> Post([FromBody] ShopManageModel shopManagerModel)
        {
            var responseModel = await _shopService.CreateShopAsync(shopManagerModel);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] ShopManageModel shopManageModel)
        {
            var responseModel = await _shopService.UpdateShopAsync(id, shopManageModel);
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
            var responseModel = await _shopService.DeleteShopAsync(id);
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
