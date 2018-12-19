using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using smartCubes.ViewModels.Activity;
using Xamarin.Forms;

namespace smartCubes.View.Activity
{
    public partial class AddDevicePopUp : PopupPage
    {
        public AddDevicePopUp(EditActivityViewModel newActivityViewModel)
        {
            InitializeComponent();
            BindingContext = new AddDevicePopUpViewModel(newActivityViewModel);
        }
        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return Content.FadeTo(1);
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return Content.FadeTo(1);
        }
    }
}
