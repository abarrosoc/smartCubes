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

        public Home()
        {
            InitializeComponent();
           
            BindingContext = new HomeViewModel(Navigation);
        }
    }
}
