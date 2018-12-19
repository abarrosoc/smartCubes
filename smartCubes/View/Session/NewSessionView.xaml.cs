using System;
using System.Collections.Generic;
using smartCubes.Models;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class NewSessionView : ContentPage
    {
        public NewSessionView(UserModel user, bool modify, SessionModel session)
        {
            InitializeComponent();

            BindingContext = new EditSessionViewModel(user,modify,session);
        }
    }
}
