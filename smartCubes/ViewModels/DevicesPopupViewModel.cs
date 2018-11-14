using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;

namespace smartCubes.ViewModels
{
    public class DevicesPopupViewModel : BaseViewModel
    {

        public DevicesPopupViewModel()
        {
        }
        private ObservableCollection<IDevice> _DevicesList;

        public ObservableCollection<IDevice> DevicesList
        {
            get
            {
                return _DevicesList;

            }
            set
            {
                _DevicesList = value;
                RaisePropertyChanged("DevicesList");
            }
        }


    }
}

