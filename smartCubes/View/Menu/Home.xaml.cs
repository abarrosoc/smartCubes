using System;
using System.Collections.Generic;
using smartCubes.ViewModels.Menu;
using Xamarin.Forms;
using smartCubes.View.Activity;
using System.Diagnostics;
using smartCubes.View.Session;
using smartCubes.Utils;
using System.Threading.Tasks;
using smartCubes.Models;

namespace smartCubes.View.Menu
{
    public partial class Home : ContentPage
    {
        private UserModel user;
        public Home()
        {
            InitializeComponent();
        }

        public Home(UserModel user)
        {
            this.user = user;
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation, user);

        }

        protected override void OnAppearing()
        {            
            var vm = BindingContext as HomeViewModel;
            vm?.RefreshData();
            base.OnAppearing();

        }
    }
}
