using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public IList<Product> Products { get; set; }
        public decimal Shipping { get; set; }
        public decimal Tax { get; set; }
    }
}
