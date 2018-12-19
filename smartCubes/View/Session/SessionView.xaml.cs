using smartCubes.ViewModels.Session;
using smartCubes.Models;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class SessionView : ContentPage
    {
        public SessionView(UserModel user)
        {
            InitializeComponent();

            BindingContext = new SessionViewModel(Navigation,user);
        }
    }
}
