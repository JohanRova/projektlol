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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoldStarr_YSYS_OP1_Grupp_6.InFramePages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StockPage : Page
    {
        private Store store;
        private ObservableCollection<Merchandise> MerchandiseList;

        public StockPage()
        {
            this.InitializeComponent();
            ChangeStockNoInput.Visibility = Visibility.Collapsed;
            NoListInput.Visibility = Visibility.Collapsed;
            WrongInputNotANumber.Visibility = Visibility.Collapsed;
            TempStores.ResetProperties();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ChangeStockBox.Visibility = Visibility.Collapsed;
        }

        int index;
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = MerchandiseListListView.SelectedIndex;
            TempStores.MerchandiseIndexTemp = MerchandiseListListView.SelectedIndex;
        }

        private void onClickStockEnter(object sender, RoutedEventArgs e)
        {
            if (TempStores.MerchandiseIndexTemp >= 0)
            {
                if (!string.IsNullOrWhiteSpace(AmountBox.Text))
                {
                    Merchandise merchtemp = MerchandiseList[index];
                    merchtemp.IncreaseStock(Int32.Parse(AmountBox.Text));
                 
                    MerchandiseList.Insert(index, merchtemp);
                    MerchandiseList.RemoveAt(index + 1);
                    ChangeStockNoInput.Visibility = Visibility.Collapsed;
                    NoListInput.Visibility = Visibility.Collapsed;
                    AmountBox.Text = string.Empty;
                }
                else
                {
                    ChangeStockNoInput.Visibility = Visibility.Visible;
                    NoListInput.Visibility = Visibility.Collapsed;

                }

            }
            else
            {
                NoListInput.Visibility = Visibility.Visible;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            store = (Store)e.Parameter; // get parameter
            MerchandiseList = store.MerchandiseCollection;
            ChangeStockBox.Visibility = Visibility.Visible;
        }

        public void HideExtras()
        {
            StockChangeText.Visibility = Visibility.Collapsed;
            ChangeStockText.Visibility = Visibility.Collapsed;
            AmountBox.Visibility = Visibility.Collapsed;
            EnterButton.Visibility = Visibility.Collapsed;
        }

        private async void AddNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            var tempShowAddDialogAsync = await ShowAddDialogAsync();
            if (tempShowAddDialogAsync != null && tempShowAddDialogAsync.Stock != 0)
            {
                MerchandiseList.Add(tempShowAddDialogAsync);
                MerchandiseListListView.ItemsSource = MerchandiseList;
            }
            else
            {
                WrongInputNotANumber.Visibility = Visibility.Visible;
            }
           
        }
        public static async Task<Merchandise> ShowAddDialogAsync()
        {
            var stackpanel = new StackPanel();

            var productLabel = new TextBlock();
            productLabel.Text = "Name of product";
            var productTextBox = new TextBox();
            stackpanel.Children.Add(productLabel);
            stackpanel.Children.Add(productTextBox);

            var supplierLabel = new TextBlock();
            supplierLabel.Text = "Name of supplier";
            var supplierTextBox = new TextBox();
            stackpanel.Children.Add(supplierLabel);
            stackpanel.Children.Add(supplierTextBox);

            var amountLabel = new TextBlock();
            amountLabel.Text = "Quantity";
            var amountTextBox = new TextBox();
            stackpanel.Children.Add(amountLabel);
            stackpanel.Children.Add(amountTextBox);

            var dialog = new ContentDialog
            {
                Content = stackpanel,
                Title = "Add new product: ",
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel",

            };
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                int tempQuantity;
                if(int.TryParse(amountTextBox.Text, out tempQuantity))
                {

                }
                return new Merchandise(productTextBox.Text, supplierTextBox.Text, tempQuantity);

            }
            else
            {
                return null;
            }

        }
    }
}
