
using smartCubes.Models;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class NewSessionView : ContentPage
    {
        public NewSessionView()
        {
            InitializeComponent();
        }
        public NewSessionView(INavigation navigation, UserModel user, bool modify, SessionModel session)
        {
            InitializeComponent();

            BindingContext = new EditSessionViewModel(navigation, user, modify, session);
        }
    }
}
