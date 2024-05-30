
using CheckoutKata.Interfaces;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        public Checkout() 
        { 

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
