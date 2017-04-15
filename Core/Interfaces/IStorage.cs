using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IProductStorage
    {
        IList<Product> GetAll();
        Product GetById(Guid id);
        Task CreateAsync(Product product);
    }

    public interface ICartStorage
    {
        IList<Cart> GetAll();
        Cart GetById(Guid id);
        Task<Guid?> CreateAsync();
        Task DeleteAsync(Guid cartId);
        Task AddItemAsync(Guid cartId, Guid itemId);
        Task DeleteItemAsync(Guid cartId, Guid itemId);
    }
}
