using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IProductService
    {
        IList<ProductSummary> GetAll();
        ProductSummary GetSummaryById(Guid id);
        Product GetById(Guid id);
    }

    public interface ICartService
    {
        Cart GetCart(Guid id);
        Task<Guid?> CreateEmptyAsync();
        Task<bool> AddItemAsync(Guid cartId, Guid itemId);
        Task<bool> DeleteItemAsync(Guid cartId, Guid itemId);
    }
}
