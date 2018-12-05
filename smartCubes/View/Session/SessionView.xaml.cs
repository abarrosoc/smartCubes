using System;
using System.Collections.Generic;
using System.Diagnostics;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class SessionView : ContentPage
    {
        public SessionView()
        {
            InitializeComponent();

            BindingContext = new SessionViewModel(Navigation);
        }
    }
}
