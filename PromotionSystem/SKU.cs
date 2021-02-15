namespace PromotionSystem
{
    public class SKU
    {
        public string Id { get; set; }
        public double Price { get; set; }
        public SKUDiscount PromotionDiscount { get => promotionDiscount; }

        private SKUDiscount promotionDiscount = new SKUDiscount()
        {
            SKUDiscountId = 0,
            UnitCount = 0,
            Discount = 0
        };
    }
}
