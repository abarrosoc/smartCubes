﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using smartCubes.ViewModels;
using Xamarin.Forms;

namespace smartCubes.View
{
    public partial class DevicesPopupView : PopupPage
    {
        private DevicesPopupViewModel _dpvm;

        private DateTime datetime;
        private IAdapter adapter;
        private IBluetoothLE ble;
        private ObservableCollection<IDevice> deviceList;

        public DevicesPopupView()
        {
            InitializeComponent();

            _dpvm = new DevicesPopupViewModel();
            BindingContext = _dpvm;

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            //adapter.ScanTimeout = 150000;
            lDevices.RefreshCommand = new Command(() => {
                RefreshData();
                lDevices.IsRefreshing = false;
            });
            var state = ble.State;
            ble.StateChanged += (s, e) =>
            {
                Debug.WriteLine($"The bluetooth state changed to {e.NewState}");
                SearchDevice();
            };

        }


        private async void SearchDevice()
        {

            if (ble.IsAvailable && ble.IsOn)
            {
                Debug.WriteLine("Start Scanning...");
                datetime = DateTime.Now;
                deviceList = new ObservableCollection<IDevice>();
                try
                {
                    adapter.DeviceDiscovered += (s, a) =>
                    {
                        if (!(deviceList.Contains(a.Device)) && (a.Device.ToString() != null))
                        {
                            Debug.WriteLine("Add new device: " + a.Device.Name + " ID: " + a.Device.Id);
                            deviceList.Add(a.Device);
                            lDevices.ItemsSource = deviceList;

                        }
                    };
                    await adapter.StartScanningForDevicesAsync();
                    this.BindingContext = deviceList;
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

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null)
            {
                ((ListView)sender).SelectedItem = null;
                return;
            }
            Debug.WriteLine("Seleccionado: " + e.Item);

            try
            {
                //Dispositivos seleccionado
                var deviceConnected = ((ListView)sender).SelectedItem as IDevice;
                //Conectamos con el dispositivo

                if (deviceConnected.State == DeviceState.Disconnected)
                {
                    await adapter.ConnectToDeviceAsync(deviceConnected);

                    //Mensaje en pantalla
                    await DisplayAlert(deviceConnected.Name, "Estado:" + deviceConnected.State, "OK");

                    //Servicios y caracteristicas y descriptores
                    var services = await deviceConnected.GetServicesAsync();
                    var characteristics = await services[0].GetCharacteristicsAsync();
                    //var descriptors = await characteristics[0].GetDescriptorsAsync();

                    //lectura de datos
                    characteristics[0].ValueUpdated += (s, a) =>
                    {
                        byte[] valueBytes = a.Characteristic.Value;
                        //await characteristics[0].ReadAsync(); //lee constantemente
                        String valueString = string.Concat(valueBytes.Select(b => b.ToString("X2")));
                        Debug.WriteLine(valueString, "Leyendo datos de " + deviceConnected.Name + ": ");
                    };
                    await characteristics[0].StartUpdatesAsync();

                    //var bytes = await characteristics[0].ReadAsync();
                }
                else
                {
                    //Si esta conectado, se desconecta
                    await adapter.DisconnectDeviceAsync(deviceConnected);
                    await DisplayAlert(deviceConnected.Name, "Estado:" + deviceConnected.State, "OK");
                }

            }
            catch (DeviceConnectionException ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                ((ListView)sender).SelectedItem = null; // eliminar seleccion
            }
        }

        private void RefreshData()
        {
            lDevices.ItemsSource = null;
            lDevices.ItemsSource = deviceList;
        }

        private void Onclick(object sender, EventArgs e)
        {
            Debug.WriteLine("ADD!");
        }
    }
}
