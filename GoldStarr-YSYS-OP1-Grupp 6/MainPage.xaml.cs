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
using Windows.Storage;
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

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Koden under funkar för att skapa en fil som heter "sample.txt", om filen redan finns så bara öppnas den.
            //med away fileio så kan man skriva in text, denna ska skrivas efter "storageFile, ...)"
            /*StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync("sample.txt", CreationCollisionOption.OpenIfExists);
            await Windows.Storage.FileIO.WriteTextAsync(storageFile, );*/
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync("sample.txt", CreationCollisionOption.OpenIfExists);
            var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var outputStream = stream.GetOutputStreamAt(0))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    for (int i = 0; i < store.MerchandiseCollection.Count; i++)
                    {
                        dataWriter.WriteString($"{store.MerchandiseCollection[i].ToString()}");
                        await dataWriter.StoreAsync();
                        await outputStream.FlushAsync();
                    }
                }
            }
            stream.Dispose();

        }

        List<string> newList = new List<string>();
        string[] listarray = new string[1];
        string text;

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.GetFileAsync("sample.txt");
            //string text = await FileIO.ReadTextAsync(storageFile);
            var stream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
            ulong size = stream.Size;
            using (var inputStream = stream.GetInputStreamAt(0))
            {
                using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
                {
                    uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
                    text = dataReader.ReadString(numBytesLoaded);
                }
            }
            string[] words = text.Split(',');
            for (int i = 0; i < words.Length-1; i+=3)
            {

                Merchandise tempMerch = new Merchandise(words[i], words[i+1], Int32.Parse(words[i+2]));
                store.MerchandiseCollection.Add(tempMerch);
            }
           
        }
    }
}
