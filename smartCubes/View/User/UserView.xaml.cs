using System;
using System.Collections.Generic;
using System.Diagnostics;
using smartCubes.Utils;
using smartCubes.ViewModels.User;
using Xamarin.Forms;

namespace smartCubes.View.User
{
    public partial class UserView : ContentPage
    {
        public UserView()
        {
            InitializeComponent();
            BindingContext = new UserViewModel(Navigation);
        }
    }
}
