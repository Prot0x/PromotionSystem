using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionSystem
{
    public class PromotionEngine : IPromotionEngine
    {
        #region Public Methods
        public double getTotalPrice(List<SKU> skuList)
        {
            Dictionary<string, int> quantity = new Dictionary<string, int>();
            List<char> items = new List<char>();

            foreach (SKU sku in skuList)
            {
                switch (sku.Id)
                {
                    case "A":
                    case "a":
                        quantity.TryGetValue("A", out int currentCount);
                        quantity["A"] = currentCount + 1;
                        break;
                    case "B":
                    case "b":
                        quantity.TryGetValue("B", out int currentCount1);
                        quantity["B"] = currentCount1 + 1;
                        break;
                    case "C":
                    case "c":
                        quantity.TryGetValue("C", out int currentCount2);
                        quantity["C"] = currentCount2 + 1;
                        break;
                    case "D":
                    case "d":
                        quantity.TryGetValue("D", out int currentCount3);
                        quantity["D"] = currentCount3 + 1;
                        break;
                }
            }
           
            return calcTotal(skuList, quantity);
        }
        public void setPromotions(SKU item, SKUDiscount discount)
        {
            if (item == null)
                return;
            if (discount == null)
                return;

            item.PromotionDiscount.SKUDiscountId = discount.SKUDiscountId;
            item.PromotionDiscount.UnitCount = discount.UnitCount;
            item.PromotionDiscount.Discount = discount.Discount;
        }
        public void setPromotions(List<SKU> itemList, SKUDiscount discount)
        {
            foreach(SKU item in itemList)
            {
                item.PromotionDiscount.SKUDiscountId = discount.SKUDiscountId;
                item.PromotionDiscount.UnitCount = 1;
                item.PromotionDiscount.Discount = discount.Discount;
            }

        }
        #endregion

        #region Private Methods
        private double calcTotal(List<SKU> skuList, Dictionary<string, int> quantity)
        {
            List<char> items = new List<char>();
            int total = 0;

            foreach (SKU sku in skuList)
            {
                int typeId = skuList.Where(x => x.Id == sku.Id).Select(y => y.PromotionDiscount.SKUDiscountId).First();

                if (!items.Contains(char.Parse(sku.Id)))
                {
                    if (typeId == 1)
                    {
                        items.Add(char.Parse(sku.Id));
                        total += (quantity[sku.Id] / skuList.Where(x => x.Id == sku.Id).Select(y => y.PromotionDiscount.UnitCount).FirstOrDefault() *
                            skuList.Where(x => x.Id == sku.Id).Select(y => y.PromotionDiscount.Discount).FirstOrDefault()) +
                            (quantity[sku.Id] % skuList.Where(x => x.Id == sku.Id).Select(y => y.PromotionDiscount.UnitCount).FirstOrDefault() *
                            Convert.ToInt32(skuList.Where(x => x.Id == sku.Id).Select(y => y.Price).FirstOrDefault()));
                    }
                    else if (typeId == 2)
                    {
                        var item = skuList.Where(x => x.PromotionDiscount.SKUDiscountId == typeId).SelectMany(y => y.Id);
                        foreach (char i in item)
                            items.Add(i);
                        if (item.Count()==2)
                        {
                            total += quantity[sku.Id] * skuList.Where(x => x.Id == sku.Id).Select(y => y.PromotionDiscount.Discount).FirstOrDefault();
                        }
                        else
                            total += quantity[sku.Id] * Convert.ToInt32(skuList.Where(x => x.Id == sku.Id).Select(y => y.Price).FirstOrDefault());
                    }
                    else
                    {
                        items.Add(char.Parse(sku.Id));
                        total += quantity[sku.Id] * Convert.ToInt32(skuList.Where(x => x.Id == sku.Id).Select(y => y.Price).FirstOrDefault());
                    }
                }

            }
            return total;
        }
        #endregion
    }
}
