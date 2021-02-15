using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionSystem;

namespace PromotionSystemTest
{
    [TestClass]
    public class ClientTest
    {
        IPromotionEngine _promotionEngine = new PromotionEngine();
        readonly SKU item1;
        readonly SKU item2;
        readonly SKU item3;
        readonly SKU item4;

        public ClientTest()
        {
            item1 =  new SKU() { Id = "A", Price = 50 };
            item2 = new SKU() { Id = "B", Price = 30 };
            item3 = new SKU() { Id = "C", Price = 20 };
            item4 = new SKU() { Id = "D", Price = 15 };
            List<SKU> _itemList1 = new List<SKU>();
            _itemList1.Add(item3);
            _itemList1.Add(item4);

            SKUDiscount discount = new SKUDiscount() { SKUDiscountId = 1, UnitCount = 3, Discount = 130 };
            SKUDiscount discount1 = new SKUDiscount() { SKUDiscountId = 1, UnitCount = 2, Discount = 45 };
            SKUDiscount discount2 = new SKUDiscount() { SKUDiscountId = 2, Discount = 30 };

            _promotionEngine.setPromotions(item1, discount);
            _promotionEngine.setPromotions(item2, discount1);
            _promotionEngine.setPromotions(_itemList1, discount2);
        }

        [TestMethod]
        public void TestScenarioA()
        {
            #region Arrange
            List<SKU> _itemList = new List<SKU>();
            _itemList.Add(item1);

            _itemList.Add(item2);

            _itemList.Add(item3);

            var expectedAmount = 100;
            #endregion

            #region Act
            var totalAmount = _promotionEngine.getTotalPrice(_itemList);
            #endregion

            #region Assert
            Assert.AreEqual(expectedAmount, totalAmount);
            #endregion
        }
        [TestMethod]
        public void TestScenarioB()
        {
            #region Arrange
            List<SKU> _itemList = new List<SKU>();
            _itemList.Add(item1);
            _itemList.Add(item1);
            _itemList.Add(item1);
            _itemList.Add(item1);
            _itemList.Add(item1);

            _itemList.Add(item2);
            _itemList.Add(item2);
            _itemList.Add(item2);
            _itemList.Add(item2);
            _itemList.Add(item2);

            _itemList.Add(item3);

            var expectedAmount = 370;
            #endregion

            #region Act
            var totalAmount = _promotionEngine.getTotalPrice(_itemList);
            #endregion

            #region Assert
            Assert.AreEqual(expectedAmount, totalAmount);
            #endregion
        }
        [TestMethod]
        public void TestScenarioC()
        {
            #region Arrange
            List<SKU> _itemList = new List<SKU>();
            _itemList.Add(item1);
            _itemList.Add(item1);
            _itemList.Add(item1);

            _itemList.Add(item2);
            _itemList.Add(item2);
            _itemList.Add(item2);
            _itemList.Add(item2);
            _itemList.Add(item2);

            _itemList.Add(item3);

            _itemList.Add(item4);

            var expectedAmount = 280;
            #endregion

            #region Act
            var totalAmount = _promotionEngine.getTotalPrice(_itemList);
            #endregion

            #region Assert
            Assert.AreEqual(expectedAmount, totalAmount);
            #endregion
        }
    }
}
