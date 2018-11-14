using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace smartCubes.View.Menu
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            Master = new MasterView();
            Detail = new NavigationPage(new Home())
            { 
                BarBackgroundColor = Color.FromHex("#0E547C"), 
                BarTextColor = Color.White 
            };

            App.MasterDetail = this;
        }
    }
}
