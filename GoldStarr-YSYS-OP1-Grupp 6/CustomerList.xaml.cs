using GoldStarr_YSYS_OP1_Grupp_6.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoldStarr_YSYS_OP1_Grupp_6
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerList : Page
    {
        private ObservableCollection<Customer> CustomerCollection;
        public CustomerList()
        {
            this.InitializeComponent();
            PopulateCustomerList();
        }
        public void PopulateCustomerList()
        {
            CustomerCollection = new ObservableCollection<Customer>();
            CustomerCollection.Add(new Customer("Abdi Andersson", "Fosievägen, Malmö"));
            CustomerCollection.Add(new Customer("Thomas Löhr", "Regementsgatan 31, Malmö", 0734958965));
            CustomerCollection.Add(new Customer("Jony Kiiskinen", "Malmövägen, Lund", 0733456585));
            CustomerCollection.Add(new Customer("Fisnik Haliti", "Industrigatan, Malmö", 0736987456));
            CustomerCollection.Add(new Customer("Johan Rova", "Perstorp"));
            CustomerCollection.Add(new Customer("Elin Ortega", "Åkarp"));
            CustomerCollection.Add(new Customer("Christian Elm", "Mariedalsvägen 15", 0733456145));
            CustomerCollection.Add(new Customer("Carl Malmström", "Värnhem"));
            CustomerCollection.Add(new Customer("Armin Ljalic", "Lundavägen 72", 0721456125));
            CustomersX.ItemsSource = CustomerCollection;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
