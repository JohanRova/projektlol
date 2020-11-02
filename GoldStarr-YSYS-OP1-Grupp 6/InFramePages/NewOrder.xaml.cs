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

        public NewOrder()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            store = (Store)e.Parameter; // get parameter
            CustomerCollection = store.CustomerCollection;
            MerchandiseCollection = store.MerchandiseCollection;
            inNewOrderFramCustomer.Navigate(typeof(CustomerList), store);
            inNewOrderFramStock.Navigate(typeof(StockPage), store);
            

        }

        private void onClickPopulateBoxes(object sender, RoutedEventArgs e)
        {
            TextBlockMerchName.Text = store.MerchandiseCollection[TempStores.MerchandiseIndexTemp].Name;
            TextBlockStock.Text = store.MerchandiseCollection[TempStores.MerchandiseIndexTemp].Stock.ToString();
            TextBlockSupplier.Text = store.MerchandiseCollection[TempStores.MerchandiseIndexTemp].Supplier;
            TextBlockCustomerName.Text = store.CustomerCollection[TempStores.CustomerIndexTemp].Name;
            TextBlockCustomerAddress.Text = store.CustomerCollection[TempStores.CustomerIndexTemp].Address;
            if(store.CustomerCollection[TempStores.CustomerIndexTemp].PhoneNumber != null)
            {
                TextBlockCustomerNumber.Text = store.CustomerCollection[TempStores.CustomerIndexTemp].PhoneNumber;
            }
            else if(store.CustomerCollection[TempStores.CustomerIndexTemp].PhoneNumber == null)
            {
                TextBlockCustomerNumber.Text = "";
            }
            
        }
    }
}
