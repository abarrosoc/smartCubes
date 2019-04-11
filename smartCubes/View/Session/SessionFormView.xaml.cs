using System;
using System.Collections.Generic;
using smartCubes.Models;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class SessionFormView : ContentPage
    {
        public SessionFormView()
        {
            InitializeComponent();
        }
        public SessionFormView(INavigation navigation, UserModel user, bool modify, SessionModel session)
        {
            InitializeComponent();

            BindingContext = new EditSessionViewModel(navigation, user, modify, session);
        }

    }
}
