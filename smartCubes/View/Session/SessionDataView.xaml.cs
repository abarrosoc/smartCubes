using System;
using System.Collections.Generic;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class SessionDataView : ContentPage
    {
        public SessionDataView()
        {
            InitializeComponent();
            BindingContext = new SessionDataViewModel();
        }
    }
}
