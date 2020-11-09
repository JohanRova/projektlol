using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldStarr_YSYS_OP1_Grupp_6.Classes
{
    class CustomerOrder
    {
        public DateTime OrderDateTime { get; set; }
        public Customer OrderingCustomer { get; set; }
        public Merchandise OrderedProduct { get; set; }
        public int Amount { get; set; }

        // Denna metoden skapar nya klasser av typen CustomerOrder. Om lagersaldot är för litet så returneras null så detta bör has 
        //i åtanke. Metoden anropar konstruktorn för att skapa ett nytt objekt. Metoden räknar även ner produktens lagersaldo med samma
        //mängd som beställs
        public static CustomerOrder CreateOrder(Customer orderingCustomer, Merchandise orderedProduct, int orderedAmount)
        {
            if(orderedAmount <= orderedProduct.Stock)
            {
                orderedProduct.DecreaseStock(orderedAmount);
                return new CustomerOrder(orderingCustomer, orderedProduct, orderedAmount);
            }
            else
            {
                return null;
            }
        }
        public CustomerOrder(Customer customer, Merchandise merchandise, int amount)
        {
            OrderDateTime = DateTime.Now;
            OrderedProduct = merchandise;
            OrderingCustomer = customer;
            Amount = amount;
        }
    }
}
