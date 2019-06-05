using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private bool modify;
        private ActivityModel activity;

        public AddDeviceActivityViewModel(INavigation navigation, ActivityModel activityModel, bool isModified)
        {
            this.Navigation = navigation;
            this.modify = isModified;
            this.activity = activityModel;
            if (modify)
            {
                Title = "Modificar";
            }
            else
            {
                Title = "Nueva";
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

        private String _NameDevice;

        public String NameDevice
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

        private ICommand _addDeviceCommand;

        public ICommand AddDeviceCommand
        {
            get { return _addDeviceCommand ?? (_addDeviceCommand = new Command(() => AddDeviceCommandExecute())); }
        }
        private void AddDeviceCommandExecute()
        {
            if(String.IsNullOrEmpty(NameDevice) || String.IsNullOrEmpty(Uuid))
            {
                Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos los campos", "Aceptar");
            }
            else { 
                DeviceModel newDevice = new DeviceModel();
                newDevice.Name = NameDevice;
                newDevice.Uuid = Uuid;
                newDevice.State = DeviceState.Disconnected.ToString();
                lDevices.Add(newDevice);
                NameDevice = null;
                Uuid = null;
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
                if (activity.Devices != null)
                {
                    foreach (DeviceModel device in lDevices)
                    {
                        if (!activity.Devices.Contains(device))
                        {
                            activity.Devices.Add(device);
                        }
                    }
                }
                else
                {
                    activity.Devices = new List<DeviceModel>();
                    foreach (DeviceModel device in lDevices)
                    {
                        activity.Devices.Add(device);
                    }                       
                }
                await Navigation.PushAsync(new AddMessageActivityView(activity,modify));
               
            }

        }
    }
}
