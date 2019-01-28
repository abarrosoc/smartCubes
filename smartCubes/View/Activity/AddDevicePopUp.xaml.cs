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
        public AddDevicePopUp(){
            
        }
        public AddDevicePopUp(EditActivityViewModel newActivityViewModel, bool isModified)
        {
            InitializeComponent();
            BindingContext = new AddDevicePopUpViewModel(newActivityViewModel,isModified);
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
