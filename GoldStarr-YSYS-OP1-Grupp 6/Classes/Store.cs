using GoldStarr_YSYS_OP1_Grupp_6.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace GoldStarr_YSYS_OP1_Grupp_6
{
    class Store
    {

        public ObservableCollection<Merchandise> MerchandiseCollection;
        public ObservableCollection<Customer> CustomerCollection;
        public ObservableCollection<CustomerOrder> CustomerOrderCollection;
        private bool SaveFilesFound;
        private string errorMessage;

        public Store()
        {
            MerchandiseCollection = new ObservableCollection<Merchandise>();
            CustomerCollection = new ObservableCollection<Customer>();
            CustomerOrderCollection = new ObservableCollection<CustomerOrder>();
          //  PopulatateMerchandiseCollection();
            //PopulateCustomerList();
            PopulateCustomerOrderList();
        }



        public void PopulatateMerchandiseCollection()
        {
            MerchandiseCollection.Add(new Merchandise("Marabou Schweizernöt", "Marabou", 15));
            MerchandiseCollection.Add(new Merchandise("Coca Cola", "Spendrups", 12));
            MerchandiseCollection.Add(new Merchandise("Apelsinejuice", "Festis", 13));
            MerchandiseCollection.Add(new Merchandise("Citronjuice", "Rynkeby", 5));
            MerchandiseCollection.Add(new Merchandise("Skånerost", "Zoega's", 5));
            MerchandiseCollection.Add(new Merchandise("Hallonsaft", "BOB", 15));
            MerchandiseCollection.Add(new Merchandise("Fanta", "Coca Cola Company", 125));
        }

        public void PopulateCustomerList()
        {
            CustomerCollection.Add(new Customer("Abdi Anderson", "Limhamnsvägen 27, Malmö"));
            CustomerCollection.Add(new Customer("Thomas Shelby", "Regementsgatan 31, Malmö", "0734958965"));
            CustomerCollection.Add(new Customer("Fallon Carrington", "Malmövägen 15, Lund", "0733456585"));
            CustomerCollection.Add(new Customer("Rick Grimes", "Industrigatan 32, Malmö", "0736987456"));
            CustomerCollection.Add(new Customer("Meredith Grey", "Alnarpsvägen 40, Åkarp"));
            CustomerCollection.Add(new Customer("Richard Hendricks", "Mariedalsvägen 15, Malmö", "0733456145"));
            CustomerCollection.Add(new Customer("Arthur Shelby", "Lommavägen 13, Oxie"));
            
        }

        public void PopulateCustomerOrderList()
        {
            CustomerOrderCollection.Add(new CustomerOrder(new Customer("Elin Ortega", "Rapphönevägen 23"), new Merchandise("Hallon", "Ica"), 1));
            CustomerOrderCollection.Add(new CustomerOrder(new Customer("Eva", "Ringsuvevägen 2"), new Merchandise("Blåbär", "Ica"), 3));
            CustomerOrderCollection.Add(new CustomerOrder(new Customer("Christian", "Malmövägen 13"), new Merchandise("Mango", "Ica"), 2));
        }

        private void AutoSaveToFileTimer()
        {
            DispatcherTimer dispatcherTimer;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += AutosaveToFile;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        void AutosaveToFile(object sender, object e)
        {
            SaveCustomersToFile();
            SaveMerchandiseStockToFile();
        }
        public async void SaveMerchandiseStockToFile()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync("MerchandiseSaveFile.sav", CreationCollisionOption.OpenIfExists);
            var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var outputStream = stream.GetOutputStreamAt(0))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    for (int i = 0; i < MerchandiseCollection.Count; i++)
                    {
                        dataWriter.WriteString($"{MerchandiseCollection[i].ToString()}\n");
                        await dataWriter.StoreAsync();
                        await outputStream.FlushAsync();
                    }
                }
            }
            stream.Dispose();
        }
        public async void LoadMerchandiseStockToFile()
        {
            if (SaveFilesFound)
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await storageFolder.GetFileAsync("MerchandiseSaveFile.sav");
                string text;
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
                text = text.Replace("\n", string.Empty);
                string[] words = text.Split('%');
                for (int i = 0; i < words.Length - 1; i += 3)
                {

                    Merchandise tempMerch = new Merchandise(words[i], words[i + 1], Int32.Parse(words[i + 2]));
                    MerchandiseCollection.Add(tempMerch);
                }
            }
        }
        public async void SaveCustomersToFile()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync("CustomerSaveFile.sav", CreationCollisionOption.OpenIfExists);
            var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var outputStream = stream.GetOutputStreamAt(0))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    for (int i = 0; i < CustomerCollection.Count; i++)
                    {
                        dataWriter.WriteString($"{CustomerCollection[i].ToString()}\n");
                        await dataWriter.StoreAsync();
                        await outputStream.FlushAsync();
                    }
                }
            }
            stream.Dispose();
        }
        public async void LoadCustomersFromFile()
        {
            if (SaveFilesFound)
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await storageFolder.GetFileAsync("CustomerSaveFile.sav");
                string text;
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
                text = text.Replace("\n", string.Empty);
                string[] words = text.Split('%');
                for (int i = 0; i < words.Length - 1; i += 3)
                {

                    Customer tempCustomer = new Customer(words[i], words[i + 1], words[i + 2]);
                    CustomerCollection.Add(tempCustomer);
                }
            }
        }
        public async void TryFindingSaves()
        {
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await storageFolder.GetFileAsync("CustomerSaveFile.sav");
                SaveFilesFound = true;
                TempStores.SaveFilesFound = true;
            }
            catch(System.IO.FileNotFoundException e)
            {
                errorMessage = e.Message;
            }
        }
        public string GetErrors()
        {
            return errorMessage;
        }

    }
}
