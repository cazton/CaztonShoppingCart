using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureDocumentDb.Models;
using Core.Interfaces;
using Core.Models;

namespace AzureDocumentDb.Manager
{
    public class CartStorageManager : ICartStorage
    {
        private readonly ICartCollection _cartCollection;
        private readonly IProductCollection _productCollection;


        public CartStorageManager(ICartCollection cartCollection, IProductCollection productCollection)
        {
            _cartCollection = cartCollection;
            _productCollection = productCollection;
        }

        #region ICartStorage Methods

        public IList<Cart> GetAll()
        {
            var entities = _cartCollection
                .SetupBaseQuery<ICartCollection>()
                .RunQuery();

            //You should use Automapper for this
            return entities.Select(cart => new Cart
            {
                Id = cart.Id,
                Products = cart.Products.Select(x => new Product
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    Price = x.Price
                }).ToList()
            }).ToList();
        }

        public Cart GetById(Guid id)
        {
            var entity = _cartCollection
                .SetupBaseQuery<ICartCollection>()
                .GetById(id)
                .RunQuery()
                .FirstOrDefault();

            return new Cart
            {
                Id = entity.Id,
                Products = entity.Products.Select(x => new Product
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Image = x.Image,
                        Price = x.Price
                    }).ToList()
            };
        }

        public async Task<Guid?> CreateAsync()
        {
            var entity = new CartEntity();
            if (await _cartCollection.CreateAsync(entity.Init()))
                return entity.Id;

            throw new Exception("Cart unable to be created.");
        }

        public async Task AddItemAsync(Guid cartId, Guid itemId)
        {
            var cartEntity = GetCart(cartId);
            var productEntity = GetProduct(itemId);

            cartEntity.Products.Add(productEntity);

            if (!await _cartCollection.UpdateAsync(cartEntity, cartId.ToString()))
                throw new Exception("Unable to add item to cart.");
        }

        public async Task DeleteItemAsync(Guid cartId, Guid itemId)
        {
            var cartEntity = GetCart(cartId);
            var productEntity = GetProduct(itemId);

            cartEntity.Products.Remove(productEntity);

            if (!await _cartCollection.UpdateAsync(cartEntity, cartId.ToString()))
                throw new Exception("Unable to remove item from cart.");

            throw new Exception("Unable to remove item from cart.");
        }

        public async Task DeleteAsync(Guid cartId)
        {
            await _cartCollection.DeleteAsync(cartId.ToString());
        }

        #endregion

        #region Private Methods

        private CartEntity GetCart(Guid cartId)
        {
            var entity = _cartCollection.SetupBaseQuery<ICartCollection>().GetById(cartId).RunQuery().FirstOrDefault();
            if (entity == null)
                throw new Exception("Unable to find cart to update.");

            return entity;
        }

        private ProductEntity GetProduct(Guid itemId)
        {
            var entity = _productCollection.SetupBaseQuery<IProductCollection>().GetById(itemId).RunQuery().FirstOrDefault();
            if (entity == null)
                throw new Exception("Unable to find item.");

            return entity;
        }

        #endregion
    }
}
