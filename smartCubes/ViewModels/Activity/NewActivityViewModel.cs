using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using smartCubes.Models;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Activity
{
    public class NewActivityViewModel : BaseViewModel
    {
        private DateTime datetime;
        private IAdapter adapter;
        private IBluetoothLE ble;
       // private ObservableCollection<DeviceModel> deviceList;

        public NewActivityViewModel()
        {
            Title = "Añadir";
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            //adapter.ScanTimeout = 150000;
            lDevices = new ObservableCollection<DeviceModel>();
            /*lDevices.RefreshCommand = new Command(() => {
                //RefreshData();
                lDevices.IsRefreshing = false;
            });*/
            var state = ble.State;
            ble.StateChanged += (s, e) =>
            {
                Debug.WriteLine($"The bluetooth state changed to {e.NewState}");
                SearchDevice();
            };
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

       
        private string _ColorName;

        public string ColorName
        {
            get
            {
                return _ColorName;
            }
            set
            {
                _ColorName = value;
                RaisePropertyChanged();
            }
        }

        private async void SearchDevice()
        {

            if (ble.IsAvailable && ble.IsOn)
            {
                Debug.WriteLine("Start Scanning...");
                datetime = DateTime.Now;
                //deviceList = new ObservableCollection<DeviceModel>();
                try
                {
                    adapter.DeviceDiscovered += (s, a) =>
                    {
                        if (!(lDevices.Contains(new DeviceModel(1L, a.Device.Name, a.Device.State.ToString(), a.Device.Id.ToString()))) 
                            && (a.Device.ToString() != null))
                        {
                            Debug.WriteLine("Add new device: " + a.Device.Name + " ID: " + a.Device.Id);
                            lDevices.Add(new DeviceModel(1L,a.Device.Name,a.Device.State.ToString(),a.Device.Id.ToString()));
                            //lDevices.ItemsSource = deviceList;

                        }
                    };
                    await adapter.StartScanningForDevicesAsync();
                    //this.BindingContext = deviceList;
                }
                catch (DeviceConnectionException er)
                {
                    Debug.WriteLine("ERROR: " + er.Message);
                }
                catch (Exception er)
                {

                    Debug.WriteLine("ERROR: " + er.Message);
                }
            }
            else
            {
                Debug.WriteLine("BLE no disponible");
            }
        }

    }
}

