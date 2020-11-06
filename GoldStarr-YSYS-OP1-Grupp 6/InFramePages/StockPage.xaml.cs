using GoldStarr_YSYS_OP1_Grupp_6.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
            index = Merchandise1.SelectedIndex;
            TempStores.MerchandiseIndexTemp = Merchandise1.SelectedIndex;
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

    }
}
