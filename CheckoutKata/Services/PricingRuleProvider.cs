using CheckoutKata.Interfaces;
using CheckoutKata.Models;


namespace CheckoutKata.Services
{
    public class PricingRuleProvider : IPricingRuleProvider
    {
        private readonly List<PricingRule> _pricingRules;
        public PricingRuleProvider(List<PricingRule> pricingRules)
        {
            _pricingRules = pricingRules;
        }
        public List<PricingRule> GetPricingRules()
        {
            return _pricingRules;
        }
    }
}
