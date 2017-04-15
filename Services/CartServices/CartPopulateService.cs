using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace CartServices
{
    public class CartPopulateService
    {
        private readonly ICartStorage _cartStorage;

        public CartPopulateService(ICartStorage cartStorage)
        {
            _cartStorage = cartStorage;
            WarmupService();
        }

        private async void WarmupService()
        {
            var carts = _cartStorage.GetAll();

            if (carts.Count != 0)
                await ClearCarts(carts);
        }

        private async Task ClearCarts(IList<Cart> carts)
        {
            var tasks = carts.Select(x => _cartStorage.DeleteAsync(x.Id));
            await Task.WhenAll(tasks);
        }
    }
}
