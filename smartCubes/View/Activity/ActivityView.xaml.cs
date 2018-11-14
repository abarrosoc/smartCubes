using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using smartCubes.ViewModels.Activity;

namespace smartCubes.View.Activity
{
    public partial class ActivityView : ContentPage
    {
        private ObservableCollection<String> deviceList;

        public ActivityView()
        {
            InitializeComponent();
            deviceList = new ObservableCollection<String>();
            deviceList.Add("Actividad 1");
            deviceList.Add("Actividad 2");
            listView.ItemsSource = deviceList;
            this.BindingContext = deviceList;
            BindingContext = new ActivityViewModel();
        }

        private void OnclickNewActivity(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BLEDevices());
            Navigation.PushAsync(new NewActivityView());
        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Debug.WriteLine("Actividad selccionada");
        }

    }
}
