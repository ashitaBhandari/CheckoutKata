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

        [Fact]
        public void Scan_MultipleItemsWithSpecialPrice_ShouldReturnCorrectTotalPrice()
        {
            var checkout = new Checkout(_pricingRuleProvider);

            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");
            var total = checkout.GetTotalPrice();

            Assert.Equal(100, total); // 3A = 130, 2B = 45
        }

        [Fact]
        public void Scan_MixedItems_ShouldReturnCorrectTotalPrice()
        {
            var checkout = new Checkout(_pricingRuleProvider);

            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            var total = checkout.GetTotalPrice();

            Assert.Equal(100, total); // 3A = 130, 2B = 45, 1C = 20
        }

        [Fact]
        public void Scan_InvalidItem_ShouldThrowException()
        {
            var checkout = new Checkout(_pricingRuleProvider);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(() => checkout.Scan("Z"));
            Assert.Contains("Failed to scan item", ex.Message);
            Assert.IsType<KeyNotFoundException>(ex.InnerException);
        }

        [Fact]
        public void Scan_EmptyItem_ShouldThrowException()
        {
            var checkout = new Checkout(_pricingRuleProvider);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(() => checkout.Scan(string.Empty));
            Assert.Contains("Failed to scan item", ex.Message);
            Assert.IsType<ArgumentException>(ex.InnerException);
        }
    }
}