using System;
using System.Collections.Generic;
using System.Diagnostics;
using smartCubes.ViewModels.User;
using Xamarin.Forms;

namespace smartCubes.View.User
{
    public partial class UserView : ContentPage
    {
        public UserView()
        {
            InitializeComponent();
            BindingContext = new UserViewModel();
        }
        private void OnclickNewUser(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewUserView());
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Debug.WriteLine("Usuario selccionada");
        }
    }
}
