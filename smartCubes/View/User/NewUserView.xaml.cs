using System;
using System.Collections.Generic;
using smartCubes.Models;
using smartCubes.ViewModels.User;
using Xamarin.Forms;

namespace smartCubes.View.User
{
    public partial class NewUserView : ContentPage
    {
        public NewUserView(bool modify, UserModel user)
        {
            InitializeComponent();
            BindingContext = new EditUserViewModel(modify,user);
        }
    }
}
