using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using smartCubes.ViewModels.Activity;
using Xamarin.Forms;

namespace smartCubes.View.Activity
{
    public partial class AddFieldsPopUp : PopupPage
    {
        public AddFieldsPopUp(){
            InitializeComponent();
        }
        public AddFieldsPopUp(AddMessageActivityViewModel addMessageActivityViewModel, bool isModified)
        {
            InitializeComponent();
            BindingContext = new AddFieldsPopUpViewModel(addMessageActivityViewModel,isModified);
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
