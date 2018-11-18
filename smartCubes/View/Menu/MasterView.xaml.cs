using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartCubes.ViewModels.Menu;
using smartCubes.View.Activity;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using smartCubes.View.Session;
using smartCubes.View.User;

namespace smartCubes.View.Menu
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : ContentPage
    {
       
        public MasterView()
        {
            InitializeComponent();
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Sesiones",
                IconSource = "ble_settings.png",
                TargetType = typeof(SessionView)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Usuarios",
                IconSource = "users.png",
                TargetType = typeof(UserView)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Actividades",
                IconSource = "students.png",
                TargetType = typeof(ActivityView)
            });

            listView.ItemsSource = masterPageItems;

            BindingContext = new MasterViewModel();
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