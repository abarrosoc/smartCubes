using System;
using System.Collections.Generic;
using smartCubes.ViewModels.Menu;
using Xamarin.Forms;

namespace smartCubes.View.Menu
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            Title = "Inicio";
            BindingContext = new HomeViewModel();
        }
    }
}
