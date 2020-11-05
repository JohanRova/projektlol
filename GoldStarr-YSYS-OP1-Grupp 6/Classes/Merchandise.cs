using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldStarr_YSYS_OP1_Grupp_6.Classes
{
    public class Merchandise
    {
        public string Name { get; set; }
        public string Supplier { get; set; }
        public int Stock { get; set; }

        public override string ToString()
        {
            return $"{Name},{Supplier},{Stock},";
        }
        public Merchandise(string name, string supplier, int stock)
        {
            Name = name;
            Supplier = supplier;
            Stock = stock;
        }
        public void ChangeStock(int newStock)
        {
            Stock = newStock;
        }
        public void IncreaseStock(int increasedStock)
        {
            Stock += increasedStock;
        }
        public void DecreaseStock(int decreasedStock)
        {
            Stock -= decreasedStock;
        }
    }
}
