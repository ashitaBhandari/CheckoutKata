using CheckoutKata.Interfaces;
using CheckoutKata.Services;

namespace CheckoutKata.Tests
{
    public class CheckoutTests
    {
        private readonly IPricingRuleProvider _pricingRuleProvider;
        public CheckoutTests() 
        {
            _pricingRuleProvider = new PricingRuleProvider();
        }
        [Fact]
        public void Scan_ShouldReturnCorrectPrice()
        {
            var checkout = new Checkout(_pricingRuleProvider);

            checkout.Scan("A");
            var total = checkout.GetTotalPrice();

            Assert.Equal(100, total);
        }
    }
}