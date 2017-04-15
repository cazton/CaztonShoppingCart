using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Core.Interfaces;
using Core.Models;

namespace ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductStorage _productStorage;

        public ProductService(IProductStorage productStorage)
        {
            _productStorage = productStorage;
        }

        #region IProductService Methods

        public IList<ProductSummary> GetAll()
        {
            var products = _productStorage.GetAll();

            //Good place to use automapper
            return products.Select(x => new ProductSummary {Id = x.Id, Name = x.Name, Price = x.Price, Image = x.Image}).ToList();
        }

        public ProductSummary GetSummaryById(Guid id)
        {
            var product = _productStorage.GetById(id);

            //Good place to use automapper
            return new ProductSummary {Id = product.Id, Name = product.Name, Price = product.Price, Image = product.Image };
        }

        public Product GetById(Guid id)
        {
            return _productStorage.GetById(id);
        }

        #endregion
    }
}
