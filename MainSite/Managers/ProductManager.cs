using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using MainSite.Interfaces;

namespace MainSite.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IProductService _productService;

        public ProductManager(IProductService productService)
        {
            _productService = productService;
        }

        public IList<ProductSummary> GetAll()
        {
            return _productService.GetAll();
        }

        public ProductSummary GetById(Guid id)
        {
            return _productService.GetSummaryById(id);
        }
    }
}
