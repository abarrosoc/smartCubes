using System;
using System.Collections.Generic;
using smartCubes.ViewModels.Configuration;

using Xamarin.Forms;

namespace smartCubes.View.Configuration
{
    public partial class ConfigurationView : ContentPage
    {
        public ConfigurationView()
        {
            InitializeComponent();
            BindingContext = new ConfigurationViewModel();
        }
    }
}
