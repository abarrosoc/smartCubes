using System;
using System.Collections.Generic;
using System.Diagnostics;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.ViewModels.User;
using Xamarin.Forms;

namespace smartCubes.View.User
{
    public partial class UserView : ContentPage
    {
        public UserView(UserModel user)
        {
            InitializeComponent();
            BindingContext = new UserViewModel(Navigation, user);
        }
    }
}
