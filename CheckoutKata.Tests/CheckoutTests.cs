using CheckoutKata.Interfaces;
using CheckoutKata.Models;
using CheckoutKata.Services;

namespace CheckoutKata.Tests
{
    public class CheckoutTests
    {
        private readonly IPricingRuleProvider _pricingRuleProvider;
        private readonly List<PricingRule> _defaultPricingRules;
        public CheckoutTests() 
        {
            _defaultPricingRules = new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            };
            _pricingRuleProvider = new PricingRuleProvider(_defaultPricingRules);
        }
        [Fact]
        public void Scan_ShouldReturnCorrectPrice()
        {
            var checkout = new Checkout(_pricingRuleProvider);
            checkout.Scan("A");

            //Assert
            var total = checkout.GetTotalPrice();
            Assert.Equal(50, total);
        }
        [Fact]
        public void AddPricingRule_ShouldReturnTotalPrice()
        {
            _defaultPricingRules.Add(new PricingRule("E", 40, 4, 120));

            var checkout = new Checkout(_pricingRuleProvider);
            checkout.Scan("AABAABCDE");

            //Assert
            var total = checkout.GetTotalPrice();
            Assert.Equal(300, total);

        }
        [Fact]
        public void GetTotalPrice_NoItems_ShouldReturnZero()
        {
            var checkout = new Checkout(_pricingRuleProvider);

            // Assert
            var total = checkout.GetTotalPrice();         
            Assert.Equal(0, total);
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

            //Assert
            var total = checkout.GetTotalPrice();
            Assert.Equal(175, total); // 3A = 130, 2B = 45
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

            //Assert
            var total = checkout.GetTotalPrice();

            Assert.Equal(195, total); // 3A = 130, 2B = 45, 1C = 20  == 195
        }

        [Fact]
        public void Scan_SingleStringItem_ShouldReturnCorrectTotalPrice()
        {
            var checkout = new Checkout(_pricingRuleProvider);

            checkout.Scan("ABAABCD");

            //Assert
            var total = checkout.GetTotalPrice();
            Assert.Equal(210, total); // 3A = 130, 2B = 45, 1C = 20 , 1D = 15 == 210
        }

        [Fact]
        public void Scan_InvalidItem_ShouldThrowException()
        {
            var checkout = new Checkout(_pricingRuleProvider);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(() => checkout.Scan("Q"));
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