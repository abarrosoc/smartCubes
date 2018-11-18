using System;
using System.Collections.Generic;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.View.Session
{
    public partial class NewSessionView : ContentPage
    {
        public NewSessionView()
        {
            InitializeComponent();

            BindingContext = new NewSessionViewModel();
        }
    }
}
