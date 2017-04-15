using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class ProductSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
