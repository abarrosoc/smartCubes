using System;
using System.Collections.Generic;
using smartCubes.ViewModels.Menu;
using smartCubes.View.Activity;

using Xamarin.Forms;
using smartCubes.View.Session;
using smartCubes.View.User;
using smartCubes.Models;

namespace smartCubes.View.Menu
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : ContentPage
    {
        private UserModel user;
        public MasterView(UserModel user)
        {
            this.user = user;
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
                if(item.TargetType == typeof(SessionView)){
                    SessionView session = new SessionView(user);
                    App.MasterDetail.Detail.Navigation.PushAsync(session);
                    listView.SelectedItem = null;
                    App.MasterDetail.IsPresented = false; 
                }else{
                    App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                    listView.SelectedItem = null;
                    App.MasterDetail.IsPresented = false;
                }

            }

        }
    }

   
}