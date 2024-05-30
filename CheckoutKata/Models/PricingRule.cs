
namespace CheckoutKata.Models
{
    public class PricingRule
    {
        public string SKU { get; private set; }
        public int UnitPrice { get; private set; }
        public int? SpecialQuantity { get; private set; }
        public int? SpecialPrice { get; private set; }

        public PricingRule(string sku, int unitPrice, int? specialQuantity = null, int? specialPrice = null)
        {
            SKU = sku;
            UnitPrice = unitPrice;
            SpecialQuantity = specialQuantity;
            SpecialPrice = specialPrice;
        }
    }
}