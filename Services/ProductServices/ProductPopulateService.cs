using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace ProductServices
{
    public class ProductPopulateService
    {
        private readonly IProductStorage _productStorage;

        public ProductPopulateService(IProductStorage productStorage)
        {
            _productStorage = productStorage;
            WarmUpService();
        }

        private async void WarmUpService()
        {
            var startItems = _productStorage.GetAll();

            if (startItems.Count == 0)
                await GenerateItems();
        }

        private async Task GenerateItems()
        {
            var random = new Random();
            var products = new List<Product>();

            for (int i = 0; i < 25; i++)
            {
                products.Add(new Product
                {
                    Name = $"Product {i}",
                    Description = "Cupcake ipsum dolor sit. Amet I love liquorice jujubes pudding croissant I love pudding. Apple pie macaroon toffee jujubes pie tart cookie applicake caramels. Halvah macaroon I love lollipop. Wypas I love pudding brownie cheesecake tart jelly-o. Bear claw cookie chocolate bar jujubes toffee",
                    Price = random.Next(1000, 5000),
                    Image = "Computer"
                });
            }

            var tasks = products.Select(_productStorage.CreateAsync);
            await Task.WhenAll(tasks);
        }
    }
}
