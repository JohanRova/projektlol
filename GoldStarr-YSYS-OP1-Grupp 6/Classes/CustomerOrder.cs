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

        public CustomerOrder(Customer customer, Merchandise merchandise, int amount)
        {
            OrderDateTime = DateTime.Now;
            OrderedProduct = merchandise;
            OrderingCustomer = customer;
            Amount = amount;
        }

        public string ConvertToSaveData()
        {
            return $"{OrderDateTime}¤{OrderingCustomer}¤{OrderedProduct}¤{Amount}";
        }
    }
}
