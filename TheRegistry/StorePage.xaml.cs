using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TheRegistry.Model;
using TheRegistry.Persistence;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TheRegistry
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StorePage : Page
    {
        public StorePage()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StoreEditPage));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            string storeCode = (string)(((Button)sender).Tag);
            this.Frame.Navigate(typeof(StoreEditPage), storeCode);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string storeCode = (string)(((Button)sender).Tag);
            DataContainer.GetInstance().StoreList.Remove(
                DataContainer.GetInstance().StoreList.First(x => x.StoreCode == storeCode)
                );
            itemsControl.ItemsSource = null;
            itemsControl.ItemsSource = DataContainer.GetInstance().StoreList;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            itemsControl.ItemsSource = DataContainer.GetInstance().StoreList;
        }
    }
}
 