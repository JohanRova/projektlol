using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldStarr_YSYS_OP1_Grupp_6.Classes
{
    public static class TempStores
    {
        public static int CustomerIndexTemp { get; set; }
        public static int MerchandiseIndexTemp { get; set; }
        public static void ResetProperties()
        {
            CustomerIndexTemp = -1;
            MerchandiseIndexTemp = -1;
        }
    }
}
