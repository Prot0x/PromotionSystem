using System.Collections.Generic;

namespace PromotionSystem
{
    public interface IPromotionEngine
    {
        void setPromotions(SKU item, SKUDiscount discount);
        void setPromotions(List<SKU> itemList, SKUDiscount discount);
        double getTotalPrice(List<SKU> skuList);
    }
}
