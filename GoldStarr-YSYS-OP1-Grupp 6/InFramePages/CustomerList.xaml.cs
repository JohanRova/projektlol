using GoldStarr_YSYS_OP1_Grupp_6.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
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
        private Store store;
        private ObservableCollection<Customer> CustomerCollection;
        public CustomerList()
        {
            this.InitializeComponent();
            TempStores.ResetProperties();

        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TempStores.CustomerIndexTemp = CustomerListListView.SelectedIndex;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            store = (Store)e.Parameter; // get parameter
            CustomerCollection = store.CustomerCollection;
            CustomerListListView.ItemsSource = CustomerCollection;
        }

        private async void AddNewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var tempShowAddDialogAsync = await ShowAddDialogAsync();
            if (tempShowAddDialogAsync != null)
            {
                CustomerCollection.Add(tempShowAddDialogAsync);
                CustomerListListView.ItemsSource = CustomerCollection;
            }
           
        }
        public static async Task<Customer> ShowAddDialogAsync()
        {
            var stackpanel = new StackPanel();

            var nameLabel = new TextBlock();
            nameLabel.Text = "Name";
            var nameTextBox = new TextBox();
            stackpanel.Children.Add(nameLabel);
            stackpanel.Children.Add(nameTextBox);

            var addressLabel = new TextBlock();
            addressLabel.Text = "Address";
            var addressTextBox = new TextBox();
            stackpanel.Children.Add(addressLabel);
            stackpanel.Children.Add(addressTextBox);

            var phoneLabel = new TextBlock();
            phoneLabel.Text = "Phone number";
            var phoneTextBox = new TextBox();
            stackpanel.Children.Add(phoneLabel);
            stackpanel.Children.Add(phoneTextBox);

            var dialog = new ContentDialog
            {
                Content = stackpanel,
                Title = "Add new Customer: ",
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel",
 
            };
            if(await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                return new Customer(nameTextBox.Text, addressTextBox.Text, phoneTextBox.Text);
            }
            else 
            {
                return null;    
            }
            
        }

    }
}
