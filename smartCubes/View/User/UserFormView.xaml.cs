using System;
using System.Collections.Generic;
using smartCubes.Models;
using smartCubes.ViewModels.User;
using Xamarin.Forms;

namespace smartCubes.View.User
{
    public partial class UserFormView : ContentPage
    {
        public UserFormView(){
            InitializeComponent();
        }

        public UserFormView(INavigation navigation,UserModel userLogin, bool modify, UserModel user)
        {
            InitializeComponent();
            BindingContext = new EditUserViewModel(navigation,userLogin,modify,user);
        }
    }
}
