using System;
using System.Collections.Generic;
using smartCubes.ViewModels.Menu;
using smartCubes.View.Activity;

using Xamarin.Forms;
using smartCubes.View.Session;
using smartCubes.View.User;
using smartCubes.Models;
using smartCubes.Enum;

namespace smartCubes.View.Menu
{
    public partial class MasterView : ContentPage
    {
  
        public MasterView(UserModel user)
        {
            InitializeComponent();
            BindingContext = new MasterViewModel(Navigation, user);

        }

        /*private void OnItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
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

        }*/
    }

   
}