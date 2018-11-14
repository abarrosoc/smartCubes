using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using smartCubes.ViewModels.Activity;
using smartCubes.View.Session;

namespace smartCubes.View.Activity
{
    public partial class ActivityView : ContentPage
    {

        public ActivityView()
        {
            InitializeComponent();
            BindingContext = new ActivityViewModel();
        }

        private void OnclickNewActivity(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BLEDevices());
            Navigation.PushAsync(new NewSessionView());
        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Debug.WriteLine("Actividad selccionada");
        }

    }
}
