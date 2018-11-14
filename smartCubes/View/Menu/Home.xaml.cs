using System;
using System.Collections.Generic;
using smartCubes.ViewModels.Menu;
using Xamarin.Forms;
using smartCubes.View.Activity;
using System.Diagnostics;
using smartCubes.View.Session;

namespace smartCubes.View.Menu
{
    public partial class Home : ContentPage
    {

        public Home()
        {
            InitializeComponent();
            Title = "Inicio";
            BindingContext = new HomeViewModel();
        }
         
        private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Debug.WriteLine("Sesion seleccionada");
            
            Navigation.PushAsync(new PlaySessionView());
        }

        private void OnclickNewActivity(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BLEDevices());
            Navigation.PushAsync(new NewSessionView());
        }
    }
}
