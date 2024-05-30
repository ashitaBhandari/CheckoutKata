using CheckoutKata.Interfaces;
using CheckoutKata.Models;


namespace CheckoutKata.Services
{
    public class PricingRuleProvider : IPricingRuleProvider
    {
        public List<PricingRule> GetPricingRules()
        {
            return new List<PricingRule>
            {
                new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                new PricingRule { SKU = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                new PricingRule { SKU = "C", UnitPrice = 20 },
                new PricingRule { SKU = "D", UnitPrice = 15 }
            };
        }
    }
}
