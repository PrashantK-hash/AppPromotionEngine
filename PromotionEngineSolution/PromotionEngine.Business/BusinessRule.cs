﻿using PromotionEngine.DataAccess;
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
            
            return 10;
        }

        #region "Private Method"
        private List<ShoppingCartItem> GetMatchedCartItemToPromotion(ShoppingCart shoppingCart)
        {
            List<ShoppingCartItem> matchedItems = new List<ShoppingCartItem>();

            return matchedItems;
        }
        private List<ShoppingCartItem> GetNotMatchedCartItemToPromotion(ShoppingCart shoppingCart, List<ShoppingCartItem> matchedCartItems)
        {
            List<ShoppingCartItem> notMatchedItems = new List<ShoppingCartItem>();

            return notMatchedItems;
        }
        #endregion

    }
}
