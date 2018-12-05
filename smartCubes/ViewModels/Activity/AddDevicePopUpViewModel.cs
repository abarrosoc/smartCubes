using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Activity;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Activity
{
	public class AddDevicePopUpViewModel : BaseViewModel
    {
        public NewActivityViewModel newActivityView { get; set; }

        public AddDevicePopUpViewModel(NewActivityViewModel newActivityView)
        {
            this.newActivityView = newActivityView;
        }

        private String _Name;

        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                RaisePropertyChanged();
            }
        }

        private String _Uuid;

        public String Uuid
        {
            get
            {
                return _Uuid;
            }
            set
            {
                _Uuid = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _AddCommand;

        public ICommand AddCommand
        {
            get { return _AddCommand ?? (_AddCommand = new Command(() => OnButtonAddDeviceClickedExecute())); }
        }
        private async void OnButtonAddDeviceClickedExecute()
        {
            if(String.IsNullOrEmpty(Name) || String.IsNullOrWhiteSpace(Name) || String.IsNullOrEmpty(Uuid) || String.IsNullOrWhiteSpace(Uuid)){
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos lo campos", "OK");
                return;
            }
            DeviceModel device = new DeviceModel();
            device.Name = Name;
            device.State = "Disconnected";
            device.Uuid = Uuid;

            if(newActivityView.lDevices == null){
                newActivityView.lDevices = new ObservableCollection<DeviceModel>();
            }

            newActivityView.lDevices.Add(device);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
