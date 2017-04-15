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
    public class ProductStorageManager : IProductStorage
    {
        private readonly IProductCollection _productCollection;

        public ProductStorageManager(IProductCollection productCollection)
        {
            _productCollection = productCollection;
        }

        public IList<Product> GetAll()
        {
            var entities = _productCollection
                .SetupBaseQuery<IProductCollection>()
                .OrderByName()
                .RunQuery();
            
            //You should use Automapper for this
            return entities.Select(x => new Product{ Id = x.Id, Name = x.Name, Description = x.Description, Image = x.Image, Price = x.Price }).ToList();
        }

        public Product GetById(Guid id)
        {
            var entity = _productCollection
                .SetupBaseQuery<IProductCollection>()
                .GetById(id)
                .RunQuery()
                .FirstOrDefault();

            //You should use Automapper for this
            return new Product
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Image = entity.Image,
                Price = entity.Price
            };
        }

        public async Task CreateAsync(Product product)
        {
            var entity = new ProductEntity
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image
            };

            await _productCollection.CreateAsync(entity.Init());
        }
    }
}
