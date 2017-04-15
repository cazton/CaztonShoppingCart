using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainSite.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace MainSite.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartManager _cartManager;

        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet, Route("{id:guid}")]
        public IActionResult GetCart(Guid id)
        {
            var cart = _cartManager.GetCart(id);

            if (cart != null)
                return Ok(cart);

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var cartId = await _cartManager.CreateAsync();

            if (cartId.HasValue)
                return Ok(cartId);

            return BadRequest();
        }

        [HttpPut, Route("{cartId:guid}/item/{itemId:guid}")]
        public async Task<IActionResult> AddItemToCart(Guid cartId, Guid itemId)
        {
            if (await _cartManager.AddItemAsync(cartId, itemId))
                return Ok();

            return BadRequest();
        }

        [HttpDelete, Route("{cartId:guid}/item/{itemId:guid}")]
        public async Task<IActionResult> RemoveItemFromCart(Guid cartId, Guid itemId)
        {
            if (await _cartManager.DeleteItemAsync(cartId, itemId))
                return Ok();

            return BadRequest();
        }
    }
}