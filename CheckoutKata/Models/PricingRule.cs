
namespace CheckoutKata.Models
{
    public class PricingRule
    {
        public string? SKU { get; set; }
        public int UnitPrice { get; set; }
        public int? SpecialQuantity { get; set; }
        public int? SpecialPrice { get; set; }
    }
}