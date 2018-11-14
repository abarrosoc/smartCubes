using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class SessionView : ContentPage
    {
        public SessionView()
        {
            InitializeComponent();
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
