
using smartCubes.Models;
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
            BindingContext = new PlaySessionViewModel(session);
        }

        protected override void OnDisappearing()
        {
            var vm = BindingContext as PlaySessionViewModel;
            vm?.disconnectAll();
            //base.OnDisappearing();
        }
    }
}
