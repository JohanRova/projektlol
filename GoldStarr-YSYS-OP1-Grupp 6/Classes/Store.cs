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
        public ObservableCollection<CustomerOrder> BacklogCustomerOrderCollection;
        private bool SaveFilesFound;
        private string errorMessage = string.Empty;

        public Store()
        {
            MerchandiseCollection = new ObservableCollection<Merchandise>();
            CustomerCollection = new ObservableCollection<Customer>();
            CustomerOrderCollection = new ObservableCollection<CustomerOrder>();
            BacklogCustomerOrderCollection = new ObservableCollection<CustomerOrder>();
            /*PopulatateMerchandiseCollection();
            PopulateCustomerList();
            PopulateCustomerOrderList();*/
            AutoSaveToFileTimer();
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
            CustomerOrderCollection.Add(new CustomerOrder(CustomerCollection[1], MerchandiseCollection[1], 5));
            CustomerOrderCollection.Add(new CustomerOrder(CustomerCollection[2], MerchandiseCollection[2], 5));
            CustomerOrderCollection.Add(new CustomerOrder(CustomerCollection[3], MerchandiseCollection[3], 5));
        }

        private void AutoSaveToFileTimer()
        {
            DispatcherTimer dispatcherTimer;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += AutosaveToFile;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
        }

        void AutosaveToFile(object sender, object e)
        {
            SaveCustomersToFile();
            SaveMerchandiseStockToFile();
            SaveCustomerOrderToFile("OrderSaveFile.sav", CustomerOrderCollection);
            SaveCustomerOrderToFile("BacklogFile.sav", BacklogCustomerOrderCollection);
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

        public async void SaveOrdersToFile()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync("OrderSaveFile.sav", CreationCollisionOption.OpenIfExists);
            var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var outputStream = stream.GetOutputStreamAt(0))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    for (int i = 0; i < CustomerOrderCollection.Count; i++)
                    {
                        dataWriter.WriteString($"{CustomerOrderCollection[i].ConvertToSaveData()}%\n");
                        await dataWriter.StoreAsync();
                        await outputStream.FlushAsync();
                    }
                }
            }
            stream.Dispose();
        }
        
        public async void LoadCustomerOrdersFromFile(string SaveFileName, ObservableCollection<CustomerOrder> customerOrders)
        {
            if (SaveFilesFound)
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await storageFolder.GetFileAsync(SaveFileName);
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
                string[] words = text.Split(new char[] { '%', '¤' });
                for (int i = 0; i < words.Length - 1; i += 10)
                {
                    CustomerOrder tempOrder = new CustomerOrder(new Customer(words[i+1], words[i+2], words[i+3]), new Merchandise(words[i+5], words[i+6], Int32.Parse(words[i+7])), Int32.Parse(words[i + 9]));
                    tempOrder.OrderDateTime = DateTime.Parse(words[i]);
                    customerOrders.Add(tempOrder);
                }
            }
        }
        public async void SaveCustomerOrderToFile(string SaveFileName, ObservableCollection<CustomerOrder> customerOrders)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync(SaveFileName, CreationCollisionOption.OpenIfExists);
            var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var outputStream = stream.GetOutputStreamAt(0))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    for (int i = 0; i < customerOrders.Count; i++)
                    {
                        dataWriter.WriteString($"{customerOrders[i].ConvertToSaveData()}%\n");
                        await dataWriter.StoreAsync();
                        await outputStream.FlushAsync();
                    }
                }
            }
            stream.Dispose();
        }


        public async void TryFindingSaves(string SaveFileName)
        {
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await storageFolder.GetFileAsync(SaveFileName);
                SaveFilesFound = true;
                TempStores.SaveFilesFound = true;
            }
            catch(System.IO.FileNotFoundException e)
            {
                errorMessage += $"{e.Message}\n";
            }
        }
        public string GetErrors()
        {
            return errorMessage;
        }

    }
}
