using PromotionEngine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Entities
{
    public class PromotionDetails
    {
        public int PromotionId { get; set; }
        public string PromotionCode { get; set; }
        public string Description { get; set; }
        public int Disount { get; set; }
        public PromotionType PromotionType { get; set; }

        public PromotionCondition PromotionCondition { get; set; }
    }
}
