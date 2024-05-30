

using CheckoutKata.Models;

namespace CheckoutKata.Interfaces
{
    public interface IPricingRuleProvider
    {
        List<PricingRule> GetPricingRules();
    }
}
