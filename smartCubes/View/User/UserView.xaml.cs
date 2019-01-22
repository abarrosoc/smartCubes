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
        private UserModel user;

        public UserView(){
            
        }
        public UserView(UserModel user)
        {
            this.user = user;
            InitializeComponent();
            BindingContext = new UserViewModel(Navigation, user);
        }

        protected override void OnAppearing()
        {
            var vm = BindingContext as UserViewModel;
            vm?.RefreshData();
            base.OnAppearing();

        }
    }
}
