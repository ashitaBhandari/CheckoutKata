
using CheckoutKata.Interfaces;
using CheckoutKata.Models;
using CheckoutKata.Services;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private readonly Dictionary<string, PricingRule> _pricingRules;
        private readonly Dictionary<string, int> _items;
        public Checkout(IPricingRuleProvider pricingRuleProvider) 
        {
            try
            {
                if(pricingRuleProvider != null)
                {
                    _pricingRules = pricingRuleProvider.GetPricingRules().ToDictionary(rule => rule.SKU);
                }
                else
                {
                    throw new ArgumentNullException(nameof(pricingRuleProvider));
                }
               
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize pricing rules.", ex);
            }

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
                    totalPrice += CalculateItemPrice(item.Key, item.Value);
                }

                return totalPrice;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to calculate total price.", ex);
            }
        }

        private int CalculateItemPrice(string sku, int quantity)
        {
            var pricingRule = _pricingRules[sku];

            if (pricingRule.SpecialQuantity.HasValue && pricingRule.SpecialPrice.HasValue)
            {
                int specialCount = quantity / pricingRule.SpecialQuantity.Value;
                int regularCount = quantity % pricingRule.SpecialQuantity.Value;
                return specialCount * pricingRule.SpecialPrice.Value + regularCount * pricingRule.UnitPrice;
            }
            else
            {
                return quantity * pricingRule.UnitPrice;
            }
        }
    }
}
