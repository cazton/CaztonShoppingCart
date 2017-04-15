using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using MainSite.Interfaces;

namespace MainSite.Managers
{
    public class CartManager : ICartManager
    {
        private readonly ICartService _cartService;

        public CartManager(ICartService cartService)
        {
            _cartService = cartService;
        }

        #region ICartManager Methods

        public Cart GetCart(Guid id)
        {
            return _cartService.GetCart(id);
        }

        public async Task<Guid?> CreateAsync()
        {
            return await _cartService.CreateEmptyAsync();
        }

        public async Task<bool> AddItemAsync(Guid cartId, Guid itemId)
        {
            return await _cartService.AddItemAsync(cartId, itemId);
        }

        public async Task<bool> DeleteItemAsync(Guid cartId, Guid itemId)
        {
            return await _cartService.DeleteItemAsync(cartId, itemId);
        }

        #endregion
    }
}
