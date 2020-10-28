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

namespace GoldStarr_YSYS_OP1_Grupp_6.InFramePages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StockPage : Page
    {
        private ObservableCollection<Merchandise> MerchandiseList;

        public StockPage()
        {
            this.InitializeComponent();
            PopulateList();
        }


        public void PopulateList()
        {
            MerchandiseList = new ObservableCollection<Merchandise>();
            MerchandiseList.Add(new Merchandise("Red Bull", "Djävulen", 15));
            MerchandiseList.Add(new Merchandise("Coca cola", "Coca cola", 12));
            MerchandiseList.Add(new Merchandise("Apelsinejuice", "Grekland", 13));
            MerchandiseList.Add(new Merchandise("C#", "Microsoft", 5));
            MerchandiseList.Add(new Merchandise("Sand", "naturen", 5));
            MerchandiseList.Add(new Merchandise("Hallonsaft", "Saftblandarna", 15));
            MerchandiseList.Add(new Merchandise("Vatten", "Havet", 145));
            MerchandiseList.Add(new Merchandise("Fanta", "Coke company", 125));
            Merchandise1.ItemsSource = MerchandiseList;
        }
        
    }
}
