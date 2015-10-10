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
    public sealed partial class StoreEditPage : Page
    {
        public Store Entity { get; set; }
        private string origStoreCode;

        public StoreEditPage()
        {
            this.InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (origStoreCode != null)
            {
                //TODO: Ensure that the ID is not in use
                Store origStore = DataContainer.GetInstance().StoreList.First(x => x.StoreCode == origStoreCode);
                origStore.StoreCode = txtCode.Text;
                origStore.Description = txtDescription.Text;
                origStore.Location = txtLocation.Text;
            }
            else
            {
                //TODO: Ensure that the ID is not in use
                Store origStore = Entity;
                origStore.StoreCode = txtCode.Text;
                origStore.Description = txtDescription.Text;
                origStore.Location = txtLocation.Text;
                DataContainer.GetInstance().StoreList.Add(Entity);
            }
            this.Frame.GoBack();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string storeCode = (string)e.Parameter;
            if (storeCode != null)
            {
                Entity = DataContainer.GetInstance().StoreList.First(x => x.StoreCode == storeCode);
            }
            else
            {
                Entity = new Store();
            }
            origStoreCode = Entity.StoreCode;
        }
    }
}
