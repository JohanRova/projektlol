using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldStarr_YSYS_OP1_Grupp_6.Classes
{
   
    public class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public Customer(string name, string address, string phonenumber = null)
        {
            Name = name;
            Address = address;
            PhoneNumber = phonenumber;
        }

    /*public Customer(string name, string address, string phonenumber)
        {
            Name = name;
            Address = address;
            PhoneNumber = phonenumber;
        }*/
    }
}
