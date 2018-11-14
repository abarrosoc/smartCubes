using System;
using System.Collections.Generic;
using smartCubes.View.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class PlaySessionView : ContentPage
    {
        public PlaySessionView()
        {
            InitializeComponent();
        }

        private void OnclickShareSession(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BLEDevices());
            Navigation.PushAsync(new NewSessionView());
        }
    }
}
