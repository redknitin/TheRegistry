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
    public sealed partial class ItemEditPage : Page
    {
        public Item Entity { get; set; }
        private string origItemCode;

        public ItemEditPage()
        {
            this.InitializeComponent();                        
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string itemCode = (string)e.Parameter;
            if (itemCode != null)
            {
                Entity = DataContainer.GetInstance().ItemList.First(x => x.ItemCode == itemCode);
            }
            else
            {
                Entity = new Item();
            }
            origItemCode = Entity.ItemCode;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (origItemCode != null)
            {
                //TODO: Ensure that the ID is not in use
                Item origItem = DataContainer.GetInstance().ItemList.First(x => x.ItemCode == origItemCode);
                origItem.ItemCode = txtCode.Text;
                origItem.Description = txtDescription.Text;
                origItem.StandardPrice = decimal.Parse(txtPrice.Text);
                origItem.UnitOfMeasure = txtUom.Text;
            }
            else {
                //TODO: Ensure that the ID is not in use
                Item origItem = Entity;
                origItem.ItemCode = txtCode.Text;
                origItem.Description = txtDescription.Text;
                origItem.StandardPrice = decimal.Parse(txtPrice.Text);
                origItem.UnitOfMeasure = txtUom.Text;
                DataContainer.GetInstance().ItemList.Add(Entity);
            }
            this.Frame.GoBack();
        }
    }
}
