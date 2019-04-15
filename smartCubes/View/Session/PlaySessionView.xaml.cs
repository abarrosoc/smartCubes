
using System;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class PlaySessionView : ContentPage
    {
        public PlaySessionView()
        {
            InitializeComponent();
        }

        public PlaySessionView(SessionModel session)
        {
            InitializeComponent();
            BindingContext = new PlaySessionViewModel2(session);

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var vm = BindingContext as PlaySessionViewModel2;
            vm.DisconnectAll();
       
           
        }

        private void lostEvent(object sender, EventArgs e)
        {
           

            Device.BeginInvokeOnMainThread(() =>
            {
                this.Navigation.PopAsync();
            });
 
        }
    }
}
