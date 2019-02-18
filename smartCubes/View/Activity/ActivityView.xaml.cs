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
            BindingContext = new ActivityViewModel(Navigation);
        }
        protected override void OnAppearing()
        {
            var vm = BindingContext as ActivityViewModel;
            vm?.RefreshData();
            base.OnAppearing();

        }
    }
}
