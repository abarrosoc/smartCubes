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
        public EditActivityViewModel newActivityView { get; set; }

        private bool isModified { get; set; }

        public AddDevicePopUpViewModel(EditActivityViewModel newActivityView, bool isModified)
        {
            this.isModified = isModified;

            if(isModified){
                Name = newActivityView.SelectItem.Name;
                Uuid = newActivityView.SelectItem.Uuid;
            } 
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
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos lo campos", "Aceptar");
                return;
            }

            DeviceModel device = new DeviceModel();
            device.Name = Name;
            device.State = "Disconnected";
            device.Uuid = Uuid;

            if(newActivityView.lDevices == null){
                newActivityView.lDevices = new ObservableCollection<DeviceModel>();
            }else if(newActivityView.lDevices.Contains(device)){
                await Application.Current.MainPage.DisplayAlert("Atención", "No puede añadir dos dispositivos con el mismo uuid y/o nombre", "Aceptar");
                return;
            }
            if (isModified)
            {
                for (int i = 0; i < newActivityView.lDevices.Count ; i++)
                {
                    if (newActivityView.lDevices[i].Uuid.Equals(newActivityView.SelectItem.Uuid))
                    {
                        newActivityView.lDevices[i].Name = device.Name;
                        newActivityView.lDevices[i].Uuid = device.Uuid;
                    }
                }
                newActivityView.RefreshData();
            }else{
                newActivityView.lDevices.Add(device);
            }
            newActivityView.SelectItem = null;
           

            await PopupNavigation.Instance.PopAsync();
        }
    }
}
