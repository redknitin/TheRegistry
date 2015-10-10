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

namespace TheRegistry
{
    public sealed partial class IssueReturnPage : Page
    {
        public IssueReturnPage()
        {
            this.InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var stock = DataContainer.GetInstance().StockList;
            var thething = stock.SingleOrDefault(x => x.ItemCode==cboItem.SelectedValue.ToString());
            if (thething == null) { thething = new Stock() { ItemCode = cboItem.SelectedValue.ToString(), Quantity = decimal.Parse(txtQty.Text), StoreCode = cboStore.SelectedValue.ToString() }; stock.Add(thething); }
            else { thething.Quantity += decimal.Parse(txtQty.Text); }
            this.Frame.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cboItem.ItemsSource = DataContainer.GetInstance().ItemList;
            cboItem.DisplayMemberPath = "Description";
            cboItem.SelectedValuePath = "ItemCode";
            cboStore.ItemsSource = DataContainer.GetInstance().StoreList;
            cboStore.DisplayMemberPath = "Description";
            cboStore.SelectedValuePath = "StoreCode";
        }
    }
}
