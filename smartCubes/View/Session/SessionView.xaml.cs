using smartCubes.ViewModels.Session;
using smartCubes.Models;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class SessionView : ContentPage
    {
        private UserModel user;

        public SessionView(){
            
        }
        public SessionView(UserModel user)
        {
            this.user = user;
            InitializeComponent();
            BindingContext = new SessionViewModel(Navigation, user);
        }

        protected override void OnAppearing()
        {
            var vm = BindingContext as SessionViewModel;
            vm?.RefreshData();
            base.OnAppearing();

        }
    }
}
