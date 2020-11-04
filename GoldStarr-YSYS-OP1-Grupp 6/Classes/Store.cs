using GoldStarr_YSYS_OP1_Grupp_6.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldStarr_YSYS_OP1_Grupp_6
{
    class Store
    {

        public ObservableCollection<Merchandise> MerchandiseCollection;
        public ObservableCollection<Customer> CustomerCollection;
        public ObservableCollection<CustomerOrder> customerOrders;

        public Store()
        {
            MerchandiseCollection = new ObservableCollection<Merchandise>();
            CustomerCollection = new ObservableCollection<Customer>();
            PopulatateMerchandiseCollection();
            PopulateCustomerList();
        }



        public void PopulatateMerchandiseCollection()
        {
            MerchandiseCollection.Add(new Merchandise("Marabou Schweizernöt", "Marabou", 15));
            MerchandiseCollection.Add(new Merchandise("Coca Cola", "Spendrups", 12));
            MerchandiseCollection.Add(new Merchandise("Apelsinejuice", "Festis", 13));
            MerchandiseCollection.Add(new Merchandise("Citronjuice", "Rynkeby", 5));
            MerchandiseCollection.Add(new Merchandise("Skånerost", "Zoega's", 5));
            MerchandiseCollection.Add(new Merchandise("Hallonsaft", "BOB", 15));
            MerchandiseCollection.Add(new Merchandise("Fanta", "Coca Cola Company", 125));
        }

        public void PopulateCustomerList()
        {
            CustomerCollection.Add(new Customer("Abdi Anderson", "Limhamnsvägen 27, Malmö"));
            CustomerCollection.Add(new Customer("Thomas Shelby", "Regementsgatan 31, Malmö", "0734958965"));
            CustomerCollection.Add(new Customer("Fallon Carrington", "Malmövägen 15, Lund", "0733456585"));
            CustomerCollection.Add(new Customer("Rick Grimes", "Industrigatan 32, Malmö", "0736987456"));
            CustomerCollection.Add(new Customer("Meredith Grey", "Alnarpsvägen 40, Åkarp"));
            CustomerCollection.Add(new Customer("Richard Hendricks", "Mariedalsvägen 15, Malmö", "0733456145"));
            CustomerCollection.Add(new Customer("Arthur Shelby", "Lommavägen 13, Oxie"));
            
        }


    }
}
