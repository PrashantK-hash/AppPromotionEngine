using PromotionEngine.DataAccess;
using PromotionEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.Business
{
    public class BusinessRule
    {
        PromotionEngineData promotionEngineData;
        List<PromotionDetails> promotionDetailsData;
        List<SKU> SKUDetailsData;
        public BusinessRule()
        {
            promotionEngineData=  new PromotionEngineData();
            promotionDetailsData = promotionEngineData.GetPromotionDetails();
            SKUDetailsData = promotionEngineData.GetSKUDetails();

        }
        public decimal CalculateTotalValue(ShoppingCart shoppingCart)
        {
            var appliedPromotionItem = GetMatchedCartItemToPromotion(shoppingCart);

            return TotalValueforAppliedPromotionItem(appliedPromotionItem) + TotalValueforNonAppliedPromotionItem(shoppingCart, appliedPromotionItem);

        }

        #region "Private Method"
        private List<ShoppingCartItem> GetMatchedCartItemToPromotion(ShoppingCart shoppingCart)
        {
            List<ShoppingCartItem> matchedItems = new List<ShoppingCartItem>();
            promotionDetailsData.ForEach(sku =>
            {
                sku.PromotionCondition.Name.ForEach(x =>
                {
                    var tempMatchedItem = shoppingCart.ShoppingCartItem.Where(t => t.SKU.Name == x.Name && t.PromotionApplied == false);
                    var takeItemCount = x.ItemCount * (tempMatchedItem.Count() / x.ItemCount);
                    matchedItems.AddRange(tempMatchedItem.Take(takeItemCount).ToList());
                    foreach (var matchedItem in matchedItems.Where(y=>y.PromotionId == 0))
                    {
                        matchedItem.PromotionId = sku.PromotionId;
                        matchedItem.PromotionApplied = true;
                    }

                });
                var removePromotionId = matchedItems.GroupBy(gb => gb.PromotionId).Where(r => (r.Count() % sku.PromotionCondition.Name.Count) != 0).Select(grp => grp.Key).FirstOrDefault();
                matchedItems.RemoveAll(item => item.PromotionId == removePromotionId);
            });

            return matchedItems;
        }
        private List<ShoppingCartItem> GetNotMatchedCartItemToPromotion(ShoppingCart shoppingCart, List<ShoppingCartItem> matchedCartItems)
        {
            List<ShoppingCartItem> notMatchedItems = new List<ShoppingCartItem>();
            matchedCartItems.GroupBy(gb => gb.SKU.Name).Select(x => x.Key).ToList().ForEach(m =>
            {
                var tempMatchingData = shoppingCart.ShoppingCartItem.Where(x => x.SKU.Name == m).ToList();
                var tempCount = tempMatchingData.Count - (matchedCartItems.Where(t => t.SKU.Name == m).Count());
                notMatchedItems.AddRange(shoppingCart.ShoppingCartItem.Where(x => x.SKU.Name == m).Take(tempCount).ToList());
            });

            return notMatchedItems;
        }

        private decimal TotalValueforAppliedPromotionItem(List<ShoppingCartItem> matchedCartItem)
        {
            decimal totalAmout = 0;
            return totalAmout;
        }

        private decimal TotalValueforNonAppliedPromotionItem(ShoppingCart shoppingCart, List<ShoppingCartItem> matchedCartItem)
        {
            decimal totalAmout = 0;
            return totalAmout;
        }
        #endregion

    }
}
