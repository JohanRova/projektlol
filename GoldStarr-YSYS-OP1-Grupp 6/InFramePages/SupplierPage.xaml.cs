using System;
using System.Collections.Generic;
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
using GoldStarr_YSYS_OP1_Grupp_6.Classes;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoldStarr_YSYS_OP1_Grupp_6.InFramePages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SupplierPage : Page
    {
        private Store store;
        private ObservableCollection<Merchandise> MerchandiseCollection;

        public SupplierPage()
        {
            this.InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            store = (Store)e.Parameter; // get parameter
            MerchandiseCollection = store.MerchandiseCollection;
            SupplierListListView.ItemsSource = MerchandiseCollection;

            // add suppliers to SupplierCollection list in Store.
            for (int i = 0; i < MerchandiseCollection.Count; i++)
            {
                store.SupplierCollection.Add(MerchandiseCollection[i].Supplier);
            }

        }
    }
}
