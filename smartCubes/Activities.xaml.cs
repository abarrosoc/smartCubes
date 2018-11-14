using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace smartCubes
{
    public partial class Activities : ContentPage
    {
        private ObservableCollection<String> deviceList;

        public Activities()
        {
            InitializeComponent();
            deviceList = new ObservableCollection<String>();
            deviceList.Add("Actividad 1");
            deviceList.Add("Actividad 2");
            listView.ItemsSource = deviceList;
            this.BindingContext = deviceList;
        }

        private void OnclickNewActivity(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BLEDevices());
        }
        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {Debug.WriteLine("Actividad selccionada");
        }

    }
}
