using CheckoutKata.Models;

namespace CheckoutKata.Tests
{
    public class CheckoutTests
    {
        [Fact]
        public void Scan_ShouldReturnCorrectPrice()
        {
            var checkout = new Checkout();

            checkout.Scan("A");
            var total = checkout.GetTotalPrice();

            Assert.Equal(100, total);
        }
    }
}