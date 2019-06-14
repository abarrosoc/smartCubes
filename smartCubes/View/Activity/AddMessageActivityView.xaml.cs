using System;
using System.Collections.Generic;
using smartCubes.Models;
using smartCubes.ViewModels.Activity;
using Xamarin.Forms;

namespace smartCubes.View.Activity
{
    public partial class AddMessageActivityView : ContentPage
    {
        public AddMessageActivityView()
        {
            InitializeComponent();
        }
        public AddMessageActivityView(ActivityModel activityModel, bool isModified)
        {
            InitializeComponent();
            BindingContext = new AddMessageActivityViewModel(Navigation, activityModel, isModified);
        }
    }
}
