
using CheckoutKata.Interfaces;
using CheckoutKata.Models;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private readonly IPricingRuleProvider _pricingRules;
        public Checkout(IPricingRuleProvider pricingRules) 
        {
            _pricingRules = pricingRules;
        }

        public void Scan(string item)
        {            
        }
        public int GetTotalPrice()
        {
            return 100;
        }
    }
}
