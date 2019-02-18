using smartCubes.ViewModels.Login;
using Xamarin.Forms;

namespace smartCubes.View.Login
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }
    }
}
