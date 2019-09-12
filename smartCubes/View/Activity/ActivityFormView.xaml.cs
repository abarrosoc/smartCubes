using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;
using smartCubes.ViewModels.Activity;
using Plugin.BLE.Abstractions;
using System.Linq;
using Rg.Plugins.Popup.Services;
using smartCubes.Models;

namespace smartCubes.View.Activity
{
    public partial class ActivityFormView : ContentPage
    {
        public ActivityFormView(){
            InitializeComponent();
            
        }
        public ActivityFormView(bool modify, ActivityModel activity)
        {
            InitializeComponent();

            BindingContext = new EditActivityViewModel(Navigation, modify, activity);
        }
    }
}
