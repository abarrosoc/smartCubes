
using System;
using System.Threading;
using System.Threading.Tasks;
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

            BindingContext = new PlaySessionViewModel(session, Navigation);
        }

        protected override void OnDisappearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                base.OnDisappearing();
                var vm = BindingContext as PlaySessionViewModel;
                await vm.DisconnectAll();
            });
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private void lostEvent(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Navigation.PopAsync();
            });
 
        }
    }
}
