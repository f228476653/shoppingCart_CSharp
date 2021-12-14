using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.AggregatesModel
{
    public class CartDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ImagePath { get; set; }
    }
}
