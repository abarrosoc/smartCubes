using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartCubes.ViewModels.Menu;
using smartCubes.View.Activity;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smartCubes.View.Menu
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : ContentPage
    {
        private MasterViewModel _mvm;
       
        public MasterView()
        {
            InitializeComponent();
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Actividades",
                IconSource = "ble_settings.png",
                TargetType = typeof(ActivityView)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Gestión usuarios",
                IconSource = "home.png",
                TargetType = typeof(TabbedPage)
            });

            listView.ItemsSource = masterPageItems;

            _mvm = new MasterViewModel();
            BindingContext = _mvm;
        }

        private void OnItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {

            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                //var page = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                listView.SelectedItem = null;
                App.MasterDetail.IsPresented = false;
            }

        }
    }

   
}