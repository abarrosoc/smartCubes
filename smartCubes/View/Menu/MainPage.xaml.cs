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
            Detail = new NavigationPage(new Home(user))
            { 
                BarBackgroundColor = Color.FromHex("#3FC49A"),
                BarTextColor = Color.White 
            };

            App.MasterDetail = this;
        }
    }
}
