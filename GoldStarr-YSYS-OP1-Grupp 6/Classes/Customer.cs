using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldStarr_YSYS_OP1_Grupp_6.Classes
{
    class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }

        public Customer(string name, string address)
        {
            Name = name;
            Address = address;
        }
    public Customer(string name, string address, int phonenumber)
        {
            Name = name;
            Address = address;
            PhoneNumber = phonenumber;
        }
    }
}
