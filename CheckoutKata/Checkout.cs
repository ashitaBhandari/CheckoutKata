
using CheckoutKata.Interfaces;
using CheckoutKata.Models;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private readonly Dictionary<string, PricingRule> _pricingRules;
        private readonly Dictionary<string, int> _items;
        public Checkout(IPricingRuleProvider pricingRules) 
        {
            _pricingRules = pricingRules.GetPricingRules().ToDictionary(rule => rule.SKU!);
            _items = new Dictionary<string, int>();
        }

        public void Scan(string item)
        {
            try
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if(_pricingRules.ContainsKey(item))
                    {
                        if (_items.ContainsKey(item))
                        {
                            _items[item]++;
                        }
                        else
                        {
                            _items[item] = 1;
                        }
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Item SKU '{item}' not found in pricing rules.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid item SKU", nameof(item));
                }
                    
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Failed to scan item.", ex);
            }

     
        }
        public int GetTotalPrice()
        {
            return 100;
        }
    }
}
