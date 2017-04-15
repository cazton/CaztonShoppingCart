using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace CartServices
{
    public class CartService : ICartService
    {
        private readonly ICartStorage _cartStorage;

        public CartService(ICartStorage cartStorage)
        {
            _cartStorage = cartStorage;
        }

        public Cart GetCart(Guid id)
        {
            return _cartStorage.GetById(id);
        }

        public async Task<Guid?> CreateEmptyAsync()
        {
            try
            {
                return await _cartStorage.CreateAsync();
            }
            catch (Exception e)
            {
                //Do some logging here
                return null;
            }
        }

        public async Task<bool> AddItemAsync(Guid cartId, Guid itemId)
        {
            try
            {
                await _cartStorage.AddItemAsync(cartId, itemId);
                return true;
            }
            catch (Exception e)
            {
                //Do some logging here
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(Guid cartId, Guid itemId)
        {
            try
            {
                await _cartStorage.DeleteItemAsync(cartId, itemId);
                return true;
            }
            catch (Exception e)
            {
                //Do some logging here
                return false;
            }
        }
    }
}
