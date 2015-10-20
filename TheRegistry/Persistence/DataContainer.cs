using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TheRegistry.Model;
using Windows.Storage.Streams;

namespace TheRegistry.Persistence
{
    public class DataContainer
    {
        public List<Item> ItemList { get; set; }
        public List<Stock> StockList { get; set; }
        public List<Store> StoreList { get; set; }
        public List<Transaction> TransactionList { get; set; }

        private static DataContainer load() {
            DataContainer lInstance;

            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;

            //if (File.Exists("data.xml"))
            try
            {
                var deserializer = new XmlSerializer(typeof(DataContainer));

                var taskFile = Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("data.xml", Windows.Storage.CreationCollisionOption.OpenIfExists).AsTask();
                taskFile.Wait();

                var task = taskFile.Result.OpenStreamForReadAsync();

                //var task = folder.OpenStreamForReadAsync("ms-appdata:///local/data.xml");
                ////task.RunSynchronously();                
                //task.Wait();

                //File.OpenRead
                using (var fstream = task.Result)
                {
                    lInstance = (DataContainer)deserializer.Deserialize(fstream);
                }
            }
            catch (FileNotFoundException fnfex)
            {
                lInstance = new DataContainer()
                {
                    ItemList = new List<Item>(),
                    //ItemList.Add(new Item() { ItemCode = "ABC", Description = "Alphabet soup", StandardPrice = 13.35M, UnitOfMeasure = "can(s)" });
                    //ItemList.Add(new Item() { ItemCode = "DEF", Description = "Soup station", StandardPrice = 10M, UnitOfMeasure = "bowl(s)" });

                    StockList = new List<Stock>(),
                    //StockList.Add(new Stock() { ItemCode="ABC", StoreCode="DXB", Quantity=3m });

                    StoreList = new List<Store>(),
                    //StoreList.Add(new Store() { StoreCode="DXB", Description="Dubai" });

                    TransactionList = new List<Transaction>()
                };
            }
            catch (Exception ex) {
                lInstance = new DataContainer()
                {
                    ItemList = new List<Item>(),
                    StockList = new List<Stock>(),
                    StoreList = new List<Store>(),
                    TransactionList = new List<Transaction>()
                };
            }

            return lInstance;
        }

        private void save() {
            try {
                var serializer = new XmlSerializer(typeof(DataContainer));
                //using (var fstream = File.OpenWrite("data.xml"))

                var taskFile = Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("data.xml", Windows.Storage.CreationCollisionOption.ReplaceExisting).AsTask();
                taskFile.Wait();

                //var folder = Windows.Storage.ApplicationData.Current.LocalFolder;

                //string urlStr = "ms-appdata:///local/data.xml";
                //Uri uriObj = new Uri(urlStr);

                //try
                //{
                //    IRandomAccessStreamReference thumbnail = RandomAccessStreamReference.CreateFromUri(uriObj);
                //    var taskCreate = Windows.Storage.StorageFile.CreateStreamedFileFromUriAsync("/data.xml", uriObj, thumbnail).AsTask();
                //    taskCreate.Wait();
                //}
                //catch (Exception ex) {
                //}

                //var taskFile = Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uriObj).AsTask(); //folder.GetFileAsync("ms-appdata:///data.xml").AsTask();
                //taskFile.Wait();


                var task = taskFile.Result.OpenStreamForWriteAsync(); // folder.OpenStreamForWriteAsync("ms-appdata:///local/data.xml", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                task.Wait();

                using (var fstream = task.Result)
                {
                    serializer.Serialize(fstream, this);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void SaveToDisk() {
            save();
        }

        private DataContainer() {
            
        }

        private static DataContainer instance = null;
        public static DataContainer GetInstance() {
            return 
                (instance==null)?
                (instance=load()):
                instance;
        }
    }
}
