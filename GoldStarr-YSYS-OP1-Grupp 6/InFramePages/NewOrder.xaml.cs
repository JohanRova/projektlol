﻿using GoldStarr_YSYS_OP1_Grupp_6.Classes;
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
        private ObservableCollection<CustomerOrder> customerOrders;


        public void ClearTextBox()
        {
            OrderAmountBox.Text = string.Empty;
            
        }

        public NewOrder()
        {
            this.InitializeComponent();
            customerOrders = new ObservableCollection<CustomerOrder>();
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
                if (!string.IsNullOrWhiteSpace(tempAmountBox))
                {
                    CustomerOrder tempObj = CustomerOrder.CreateOrder(store.CustomerCollection[TempStores.CustomerIndexTemp], store.MerchandiseCollection[TempStores.MerchandiseIndexTemp], Int32.Parse(OrderAmountBox.Text));
                    if (tempObj != null)
                    {
                        customerOrders.Add(tempObj);
                        Merchandise merchtemp = MerchandiseCollection[TempStores.MerchandiseIndexTemp];
                        MerchandiseCollection.Insert(TempStores.MerchandiseIndexTemp, merchtemp);
                        MerchandiseCollection.RemoveAt(TempStores.MerchandiseIndexTemp + 1);
                        NotEnoughInStockPrompt.Visibility = Visibility.Collapsed;
                        NoAmountEnteredPrompt.Visibility = Visibility.Collapsed;
                        ListViewSelectionPrompt.Visibility = Visibility.Collapsed;
                        TempStores.MerchandiseIndexTemp = -1;
                        OrderAmountBox.Text = string.Empty;
                        //OrderMadePrompt.Visibility = Visibility.Visible;
                        OrderMadePopUp.IsOpen = true;
                        TimerSetUp();
                    }
                    else
                    {
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
