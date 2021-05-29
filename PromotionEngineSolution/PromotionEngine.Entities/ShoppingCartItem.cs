using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Entities
{
    public class ShoppingCartItem
    {
        public SKU SKU { get; set; }
        public bool PromotionApplied { get; set; }
        public int PromotionId { get; set; }

    }
}
