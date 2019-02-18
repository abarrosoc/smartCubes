using System;
using System.Collections.Generic;
using smartCubes.ViewModels.Menu;
using smartCubes.View.Activity;

using Xamarin.Forms;
using smartCubes.View.Session;
using smartCubes.View.User;
using smartCubes.Models;
using smartCubes.Enum;

namespace smartCubes.View.Menu
{
    public partial class MasterView : ContentPage
    {
        public MasterView()
        {
        }

        public MasterView(UserModel user)
        {
            InitializeComponent();
            BindingContext = new MasterViewModel(Navigation, user);
  
        }
    }
}