using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TheRegistry.Model;

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
                var task = folder.OpenStreamForReadAsync("data.xml");
                task.RunSynchronously();                
                
                //File.OpenRead
                using (var fstream = task.Result)
                {
                    lInstance =(DataContainer)deserializer.Deserialize(fstream);
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
            return lInstance;
        }

        private void save() {
            var serializer = new XmlSerializer(typeof(DataContainer));
            //using (var fstream = File.OpenWrite("data.xml"))

            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;

            var task = folder.OpenStreamForReadAsync("data.xml");
            task.RunSynchronously();

            using(var fstream = task.Result)
            {
                serializer.Serialize(fstream, this);
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
