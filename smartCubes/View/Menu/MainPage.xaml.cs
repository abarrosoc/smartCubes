using System;
using System.Collections.Generic;
using smartCubes.Models;
using Xamarin.Forms;

namespace smartCubes.View.Menu
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage(UserModel user)
        {
            InitializeComponent();
            Master = new MasterView(user);
            this.MasterBehavior = MasterBehavior.Popover;
            Detail = new NavigationPage(new Home(user))
            { 
                BarBackgroundColor = Color.FromHex("#1da1f2"),
                BarTextColor = Color.White
            };

            App.MasterDetail = this;
        }
    }
}
