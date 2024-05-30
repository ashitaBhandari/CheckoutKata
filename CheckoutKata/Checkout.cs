
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
                    if(item.Length > 1)
                    {
                        foreach(char itemChar in item)
                        {
                            InsertDataIntoDictionary(itemChar.ToString());
                        }
                    }
                    else if(item.Length == 1)
                    {
                        InsertDataIntoDictionary(item);
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

        private void InsertDataIntoDictionary(string item)
        {
            if (_pricingRules.ContainsKey(item))
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
        public int GetTotalPrice()
        {
            try
            {
                int totalPrice = 0;

                foreach (var item in _items)
                {
                    var pricingRule = _pricingRules[item.Key];
                    int itemCount = item.Value;

                    if (pricingRule.SpecialQuantity.HasValue && pricingRule.SpecialPrice.HasValue)
                    {
                        int specialCount = itemCount / pricingRule.SpecialQuantity.Value;
                        int regularCount = itemCount % pricingRule.SpecialQuantity.Value;
                        totalPrice += specialCount * pricingRule.SpecialPrice.Value + regularCount * pricingRule.UnitPrice;
                    }
                    else
                    {
                        totalPrice += itemCount * pricingRule.UnitPrice;
                    }
                }

                return totalPrice;
            }
            catch (Exception ex)
            {
                // Log or handle exception as needed
                throw new InvalidOperationException("Failed to calculate total price.", ex);
            }
        }
    }
}
