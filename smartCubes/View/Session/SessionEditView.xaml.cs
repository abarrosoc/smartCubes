
using smartCubes.Models;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class SessionEditView : ContentPage
    {
        public SessionEditView()
        {
            InitializeComponent();
        }
        public SessionEditView(INavigation navigation, UserModel user, bool modify, SessionModel session)
        {
            InitializeComponent();

            BindingContext = new EditSessionViewModel(navigation, user, modify, session);
        }
    }
}
