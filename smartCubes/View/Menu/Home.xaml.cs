using smartCubes.ViewModels.Menu;
using Xamarin.Forms;
using smartCubes.Models;

namespace smartCubes.View.Menu
{
    public partial class Home : ContentPage
    {
        private UserModel user;
        public Home()
        {
            InitializeComponent();
        }

        public Home(UserModel user)
        {
            this.user = user;
            InitializeComponent();

            BindingContext = new HomeViewModel(Navigation, user);
        }

        protected override void OnAppearing()
        {            
            var vm = BindingContext as HomeViewModel;
            vm?.RefreshData();
            base.OnAppearing();

        }
    }
}
