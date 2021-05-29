using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine.Business;
using PromotionEngine.Entities;
using System;

namespace PromotionEngine.UnitTests
{
    [TestClass]
    public class BusinessRuleTests
    {
        [TestMethod]
        public void CalculateTotalValueTest_ScenarioA()
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "A" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "C" }, PromotionId = 0 });
            var bussinessRule = new BusinessRule();
            var result = bussinessRule.CalculateTotalValue(shoppingCart);

            Assert.AreEqual(100, result);
        }
        [TestMethod]
        public void CalculateTotalValueTest_ScenarioB()
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "A" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "A" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "A" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "A" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "A" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "C" }, PromotionId = 0 });
            var bussinessRule = new BusinessRule();
            var result = bussinessRule.CalculateTotalValue(shoppingCart);

            Assert.AreEqual(370, result);
        }
        [TestMethod]
        public void CalculateTotalValueTest_ScenarioC()
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "A" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "A" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "A" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "B" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "C" }, PromotionId = 0 });
            shoppingCart.ShoppingCartItem.Add(new ShoppingCartItem() { PromotionApplied = false, SKU = new SKU() { Name = "D" }, PromotionId = 0 });
            var bussinessRule = new BusinessRule();
            var result = bussinessRule.CalculateTotalValue(shoppingCart);

            Assert.AreEqual(280, result);
        }
    }
}
