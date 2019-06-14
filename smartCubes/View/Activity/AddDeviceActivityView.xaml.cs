using System;
using System.Collections.Generic;
using smartCubes.Models;
using smartCubes.ViewModels.Activity;
using smartCubes.ViewModels.User;
using Xamarin.Forms;

namespace smartCubes.View.Activity
{
    public partial class AddDeviceActivityView : ContentPage
    {
        public AddDeviceActivityView()
        {
            InitializeComponent();
        }
        public AddDeviceActivityView( ActivityModel activityModel, bool isModified){
            InitializeComponent();
            BindingContext = new AddDeviceActivityViewModel(Navigation,activityModel,isModified );
        }
    }
}
