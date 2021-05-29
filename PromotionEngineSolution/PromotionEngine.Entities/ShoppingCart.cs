using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartItem = new List<ShoppingCartItem>();
        }
        public decimal Total { get; set; }
        public List<ShoppingCartItem> ShoppingCartItem { get; set; }
    }
}
