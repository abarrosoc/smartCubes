using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Plugin.BLE.Abstractions;
using smartCubes.Models;
using smartCubes.View.Activity;
using Xamarin.Forms;

namespace smartCubes.ViewModels.User
{
    public class AddDeviceActivityViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private bool Modify;
        private ActivityModel Activity;

        public AddDeviceActivityViewModel(INavigation navigation, ActivityModel activityModel, bool isModified)
        {
            Navigation = navigation;
            Modify = isModified;
            Activity = activityModel;

            if (Modify)
            {
                Title = "Editar actividad";
            }
            else
            {
                Title = "Nueva actividad";
            }

            lDevices = new ObservableCollection<DeviceModel>();
            if (activityModel.Devices != null)
            {
                foreach (DeviceModel device in activityModel.Devices)
                {
                    lDevices.Add(device);
                }
            }
        }

        private ObservableCollection<DeviceModel> _lDevices;
        public ObservableCollection<DeviceModel> lDevices
        {
            get
            {
                return _lDevices;
            }
            set
            {
                _lDevices = value;
                RaisePropertyChanged();
            }
        }

        private string _NameDevice;
        public string NameDevice
        {
            get
            {
                return _NameDevice;
            }
            set
            {
                _NameDevice = value;
                RaisePropertyChanged();
            }
        }
        private string _Uuid;
        public string Uuid
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

        private string _UuidService;
        public string UuidService
        {
            get
            {
                return _UuidService;
            }
            set
            {
                _UuidService = value;
                RaisePropertyChanged();
            }
        }

        private string _UuidCharacteristic;
        public string UuidCharacteristic
        {
            get
            {
                return _UuidCharacteristic;
            }
            set
            {
                _UuidCharacteristic = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _addDeviceCommand;

        public ICommand AddDeviceCommand
        {
            get { return _addDeviceCommand ?? (_addDeviceCommand = new Command(() => AddDeviceCommandExecute())); }
        }
        private void AddDeviceCommandExecute()
        {
            List<DeviceModel> lDevicesTemp = new List<DeviceModel>(lDevices);

            if(string.IsNullOrEmpty(NameDevice) || string.IsNullOrEmpty(Uuid) || string.IsNullOrEmpty(UuidService))
            {
                Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar los campos obligatorios", "Aceptar");
            }
            else if (!Modify && lDevicesTemp.Find( d => d.Name.Equals(NameDevice)) != null)
            {
                 Application.Current.MainPage.DisplayAlert("Atención", "Ya existe un dispositivo con el mismo nombre", "Aceptar");
            }
            else
            {
                string regexUuid = "[0-9a-zA-Z]{8}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{12}";

                if (!Regex.IsMatch(Uuid, regexUuid))
                {
                    Application.Current.MainPage.DisplayAlert("Atención", "El UUID del dispositivo no cumple con el formato", "Aceptar");
                    return;
                }

                if (!Regex.IsMatch(UuidService, regexUuid))
                {
                    Application.Current.MainPage.DisplayAlert("Atención", "El UUID del servicio no cumple con el formato", "Aceptar");
                    return;
                }

                if (!Regex.IsMatch(UuidCharacteristic, regexUuid))
                {
                    Application.Current.MainPage.DisplayAlert("Atención", "El UUID de la característica no cumple con el formato", "Aceptar");
                    return;
                }

                DeviceModel newDevice = new DeviceModel();
                newDevice.Name = NameDevice;
                newDevice.Uuid = Uuid;
                newDevice.Service = UuidService;
                newDevice.Characteristic = UuidCharacteristic;
                newDevice.State = DeviceState.Disconnected.ToString();
                lDevices.Add(newDevice);
                NameDevice = null;
                Uuid = null;
                UuidService = null;
                UuidCharacteristic = null;
            }
        }

        private ICommand _deleteDeviceCommand;
        public ICommand DeleteDeviceCommand
        {
            get { return _deleteDeviceCommand ?? (_deleteDeviceCommand = new Command<DeviceModel>((device) => DeleteDeviceCommandExecute(device))); }
        }
        private void DeleteDeviceCommandExecute(DeviceModel device)
        {
            lDevices.Remove(device);
        }

        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get { return _nextCommand ?? (_nextCommand = new Command(() => NextCommandExecute())); }
        }
        private async void NextCommandExecute()
        {
            if (lDevices.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe añadir al menos un dispositivo", "Aceptar");
            }
            else
            {

                Activity.Devices = new List<DeviceModel>();
                foreach (DeviceModel device in lDevices)
                {
                    Activity.Devices.Add(device);
                }                       
                
                await Navigation.PushAsync(new AddMessageActivityView(Activity,Modify));
               
            }

        }
    }
}
