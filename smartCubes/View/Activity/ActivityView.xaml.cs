using System;
using Xamarin.Forms;
using smartCubes.ViewModels.Activity;

namespace smartCubes.View.Activity
{
    public partial class ActivityView : ContentPage
    {

        public ActivityView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            BindingContext = new ActivityViewModel(Navigation);

        }
    }
}
