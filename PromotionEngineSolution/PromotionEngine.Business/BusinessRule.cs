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
            List<ShoppingCartItem> allMatchedItems = new List<ShoppingCartItem>();
            promotionDetailsData.ForEach(sku =>
            {
                List<ShoppingCartItem> matchedItems = new List<ShoppingCartItem>();
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
                allMatchedItems.AddRange(matchedItems);
            });

            return allMatchedItems;
        }
        private List<ShoppingCartItem> GetNotMatchedCartItemToPromotion(ShoppingCart shoppingCart, List<ShoppingCartItem> matchedCartItems)
        {
            List<ShoppingCartItem> notMatchedItems = new List<ShoppingCartItem>();
            if (matchedCartItems.Count() > 0)
            {
                var grpSelectedList = matchedCartItems.GroupBy(gb => gb.SKU.Name).Select(x => x.Key).ToList();
                grpSelectedList.ForEach(m =>
                {
                    var tempMatchingData = shoppingCart.ShoppingCartItem.Where(x => x.SKU.Name == m).ToList();
                    var tempCount = tempMatchingData.Count - (matchedCartItems.Where(t => t.SKU.Name == m).Count());
                    notMatchedItems.AddRange(shoppingCart.ShoppingCartItem.Where(x => x.SKU.Name == m & x.PromotionId == 0).Take(tempCount).ToList());
                });

                notMatchedItems.AddRange(shoppingCart.ShoppingCartItem.Where(x => grpSelectedList.All(p=>p!= x.SKU.Name)).ToList());
            }
            else
            {
                notMatchedItems.AddRange(shoppingCart.ShoppingCartItem);
            }

            return notMatchedItems;
        }

        private decimal TotalValueforAppliedPromotionItem(List<ShoppingCartItem> matchedCartItem)
        {
            decimal totalAmout = 0;

            matchedCartItem.GroupBy(gb => gb.PromotionId).Select(x => x.Key).ToList().ForEach(m =>
            {
                var tempDiscount = promotionDetailsData.Where(x => x.PromotionId == m).Select(p => p.Discount).FirstOrDefault();
                var tempPromotionType = promotionDetailsData.Where(x => x.PromotionId == m).Select(p => p.PromotionType).FirstOrDefault();
                var tempMatchedCount = (matchedCartItem.Where(t => t.PromotionId == m)).Count();

                var promotionItemCount = 0;

                (promotionDetailsData.Where(x => x.PromotionId == m).ToList()).ForEach(tc =>
                  {
                      tc.PromotionCondition.Name.ForEach(pc =>
                      {
                          promotionItemCount = promotionItemCount + pc.ItemCount;
                      });
                  });

                if (tempPromotionType == Common.PromotionType.FixedRateDiscount)
                {
                    totalAmout = totalAmout + (tempDiscount * (tempMatchedCount / promotionItemCount));
                }
                else if (tempPromotionType == Common.PromotionType.PercentageDiscount)
                {
                    totalAmout = totalAmout + (tempDiscount * (tempMatchedCount / promotionItemCount));
                }
            });
            return totalAmout;
        }

        private decimal TotalValueforNonAppliedPromotionItem(ShoppingCart shoppingCart, List<ShoppingCartItem> matchedCartItem)
        {
            var notAppliedPromotionItem = GetNotMatchedCartItemToPromotion(shoppingCart, matchedCartItem);
            decimal totalAmout = 0;
            notAppliedPromotionItem.ForEach(x =>
            {
                var itemPrice = SKUDetailsData.Where(y => y.Name == x.SKU.Name).Select(p => p.Price).FirstOrDefault();
                totalAmout = totalAmout + itemPrice;
            });
            return totalAmout;
        }
        #endregion

    }
}
