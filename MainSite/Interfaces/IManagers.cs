using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace MainSite.Interfaces
{
    public interface IProductManager
    {
        IList<ProductSummary> GetAll();
        ProductSummary GetById(Guid id);
    }

    public interface ICartManager
    {
        Cart GetCart(Guid id);
        Task<Guid?> CreateAsync();
        Task<bool> AddItemAsync(Guid id, Guid itemId);
        Task<bool> DeleteItemAsync(Guid cartId, Guid itemId);
    }
}
