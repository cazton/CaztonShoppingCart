using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.Config;
using MainSite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainSite.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productManager.GetAll());
        }

        [HttpGet, Route("{id:guid}")]
        public IActionResult GetProductDetails(Guid id)
        {
            var product = _productManager.GetById(id);

            if (product != null)
                return Ok(product);

            return BadRequest();
        }
    }
}