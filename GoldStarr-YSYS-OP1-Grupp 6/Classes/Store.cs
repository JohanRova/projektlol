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
            MerchandiseCollection.Add(new Merchandise("Red Bull", "Red Bull Österrike", 15));
            MerchandiseCollection.Add(new Merchandise("Coca cola", "Spendrups", 12));
            MerchandiseCollection.Add(new Merchandise("Apelsinejuice", "Ica", 13));
            MerchandiseCollection.Add(new Merchandise("Citronjuice", "Ica", 5));
            MerchandiseCollection.Add(new Merchandise("Skånerost", "Zoega's", 5));
            MerchandiseCollection.Add(new Merchandise("Hallonsaft", "BOB", 15));
            MerchandiseCollection.Add(new Merchandise("Aloe Vera", "Nobe", 145));
            MerchandiseCollection.Add(new Merchandise("Fanta", "Coke company", 125));
        }

        public void PopulateCustomerList()
        {
            CustomerCollection.Add(new Customer("Abdi Andersson", "Fosievägen, Malmö"));
            CustomerCollection.Add(new Customer("Thomas Löhr", "Regementsgatan 31, Malmö", "0734958965"));
            CustomerCollection.Add(new Customer("Jony Kiiskinen", "Malmövägen, Lund", "0733456585"));
            CustomerCollection.Add(new Customer("Fisnik Haliti", "Industrigatan, Malmö", "0736987456"));
            CustomerCollection.Add(new Customer("Johan Rova", "Perstorp"));
            CustomerCollection.Add(new Customer("Elin Ortega", "Åkarp"));
            CustomerCollection.Add(new Customer("Christian Elm", "Mariedalsvägen 15", "0733456145"));
            CustomerCollection.Add(new Customer("Carl Malmström", "Värnhem"));
            CustomerCollection.Add(new Customer("Armin Ljalic", "Lundavägen 72", "0721456125"));
        }


    }
}
