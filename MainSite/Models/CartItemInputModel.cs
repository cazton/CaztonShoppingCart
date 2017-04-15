using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainSite.Models
{
    public class CartItemInputModel
    {
        public int? CartId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
