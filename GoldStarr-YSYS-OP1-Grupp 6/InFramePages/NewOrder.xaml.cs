using GoldStarr_YSYS_OP1_Grupp_6.Classes;
using GoldStarr_YSYS_OP1_Grupp_6.InFramePages;
using GoldStarr_YSYS_OP1_Grupp_6;
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
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoldStarr_YSYS_OP1_Grupp_6
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewOrder : Page
    {
        private Store store;
        private ObservableCollection<Customer> CustomerCollection;
        private ObservableCollection<Merchandise> MerchandiseCollection;

        public void ClearTextBox()
        {
            OrderAmountBox.Text = string.Empty;         
        }

        public NewOrder()
        {
            this.InitializeComponent();
            TempStores.ResetProperties();
            OrderMadePopUp.IsOpen = false;
        }
        DispatcherTimer dispatcherTimer;

        public void TimerSetUp()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimerEventFire;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }


        void dispatcherTimerEventFire(object sender, object e)
        {
            OrderMadePopUp.IsOpen = false;
            dispatcherTimer.Stop();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            store = (Store)e.Parameter; // get parameter
            CustomerCollection = store.CustomerCollection;
            MerchandiseCollection = store.MerchandiseCollection;
            inNewOrderFramCustomer.Navigate(typeof(CustomerList), store);
            inNewOrderFramStock.Navigate(typeof(StockPage), store);
            NotEnoughInStockPrompt.Visibility = Visibility.Collapsed;
            NoAmountEnteredPrompt.Visibility = Visibility.Collapsed;
            ListViewSelectionPrompt.Visibility = Visibility.Collapsed;
            //OrderMadePrompt.Visibility = Visibility.Collapsed;
        }

        private void MakeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            string tempAmountBox = OrderAmountBox.Text;
            if ((TempStores.MerchandiseIndexTemp >= 0) && (TempStores.CustomerIndexTemp >= 0))
            {
                if (!string.IsNullOrWhiteSpace(tempAmountBox) && Int32.TryParse(tempAmountBox, out int p))
                {
                    CustomerOrder tempObj = new CustomerOrder(store.CustomerCollection[TempStores.CustomerIndexTemp], store.MerchandiseCollection[TempStores.MerchandiseIndexTemp], Int32.Parse(OrderAmountBox.Text));

                    if (tempObj != null && tempObj.Amount <= (store.MerchandiseCollection[TempStores.MerchandiseIndexTemp].Stock))
                    {

                        NotEnoughInStockPrompt.Visibility = Visibility.Collapsed;
                        NoAmountEnteredPrompt.Visibility = Visibility.Collapsed;
                        ListViewSelectionPrompt.Visibility = Visibility.Collapsed;

                        // when enough in stock for new order: 
                        // add order to list for paid and delivered orders - CustomerOrderCollection
                        // decrease merchandise stock quantity with the size of the customer order - tempObj - and update list.
                        store.CustomerOrderCollection.Add(tempObj);
                        Merchandise merchtemp = store.MerchandiseCollection[TempStores.MerchandiseIndexTemp];
                        merchtemp.DecreaseStock(tempObj.Amount);
                        store.MerchandiseCollection.Insert(TempStores.MerchandiseIndexTemp, merchtemp);
                        store.MerchandiseCollection.RemoveAt(TempStores.MerchandiseIndexTemp + 1);

                        TempStores.MerchandiseIndexTemp = -1;
                        OrderAmountBox.Text = string.Empty;
                        //OrderMadePrompt.Visibility = Visibility.Visible;
                        OrderMadePopUp.IsOpen = true;
                        TimerSetUp();
                    }
                    // when not enough in stock for new order:
                    // add the placed order to the backlog - BacklogCustomerOrderCollection
                    else if (tempObj.Amount > store.MerchandiseCollection[TempStores.MerchandiseIndexTemp].Stock)
                    {
                        store.BacklogCustomerOrderCollection.Add(tempObj);
                        NotEnoughInStockPrompt.Visibility = Visibility.Visible;
                        ListViewSelectionPrompt.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    NoAmountEnteredPrompt.Visibility = Visibility.Visible;
                    ListViewSelectionPrompt.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                ListViewSelectionPrompt.Visibility = Visibility.Visible;
            }
        }
    }
}
