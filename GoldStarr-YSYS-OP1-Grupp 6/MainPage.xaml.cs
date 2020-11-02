using GoldStarr_YSYS_OP1_Grupp_6.Classes;
using GoldStarr_YSYS_OP1_Grupp_6.InFramePages;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GoldStarr_YSYS_OP1_Grupp_6
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Store store = new Store();

        public MainPage()
        {
            this.InitializeComponent();
            TempStores.ResetProperties();
        }

        public void HideInfo()
        {
            MainPageHeader.Visibility = Visibility.Collapsed;
            MainPageInfo.Visibility = Visibility.Collapsed;
        }

        private void ButtonStock_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(StockPage), store);
            HideInfo();
        }

        private void ButtonMain_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(CustomerList), store);
            HideInfo();
        }

        private void OrderButtom_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(NewOrder), store);
            HideInfo();
        }
    }
}
