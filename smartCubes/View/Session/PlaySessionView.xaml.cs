using System;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class PlaySessionView : ContentPage
    {
        public PlaySessionView()
        {
            InitializeComponent();
            BindingContext = new PlaySessionViewModel();
        }

        private void OnclickShareSession(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BLEDevices());
            Navigation.PushAsync(new NewSessionView());
        }
    }
}
