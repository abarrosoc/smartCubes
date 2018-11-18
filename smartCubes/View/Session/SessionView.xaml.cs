using System;
using System.Collections.Generic;
using System.Diagnostics;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class SessionView : ContentPage
    {
        public SessionView()
        {
            InitializeComponent();

            BindingContext = new SessionViewModel();
        }

        private void OnclickNewActivity(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BLEDevices());
            Navigation.PushAsync(new NewSessionView());
        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Debug.WriteLine("Sesion seleccionada");
        }
    }
}
