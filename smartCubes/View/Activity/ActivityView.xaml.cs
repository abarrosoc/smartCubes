using System;
using System.Diagnostics;
using Xamarin.Forms;
using smartCubes.ViewModels.Activity;

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
            Navigation.PushAsync(new NewActivityView());
        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Debug.WriteLine("Actividad selccionada");
        }

    }
}
