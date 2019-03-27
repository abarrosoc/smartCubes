using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using smartCubes.Models;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;

namespace smartCubes.Utils
{
    public class ConnectDevices
    {
        private DateTime datetime;
        private IAdapter adapter;
        private IBluetoothLE ble;
        private List<DeviceModel> lDevices;
        private static List<DeviceData> lDeviceData;
        private List<IDevice> lDevicesConnected;


        public String StudentCode { get; set; }


        public ConnectDevices()
        {
            initBLE();
        }

        private async Task initBLE()
        {
            ActivityModel activity = Json.getActivityByName("Actividad 1");
            lDevices = new List<DeviceModel>(activity.Devices);

            lDevicesConnected = new List<IDevice>();
            lDeviceData = new List<DeviceData>();

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            adapter.ScanTimeout = 3000;


            if (!ble.State.Equals(BluetoothState.On))
            {
                ble.StateChanged += async (s, e) =>
                {
                    Debug.WriteLine("BLE state changed to " + e.NewState);
                    await SearchDevice();
                };
            }
            else
            {
                await SearchDevice();
            }
        }


        private async Task SearchDevice()
        {

            if (ble.IsAvailable && ble.IsOn)
            {
                datetime = DateTime.Now;

                if (!ble.Adapter.IsScanning)
                {
                    await CrossBluetoothLE.Current.Adapter.StartScanningForDevicesAsync();
                }

                try
                {
                    adapter.DeviceDiscovered += async (s, a) =>
                    {
                        Debug.WriteLine("Dispositivo encontrado: " + a.Device.Name + " ID: " + a.Device.Id + "Estado: " + a.Device.State);
                        foreach (DeviceModel device in lDevices)
                        {
                            if (a.Device.Name != null && device.Name.Equals(a.Device.Name) && a.Device.State.Equals(DeviceState.Disconnected))
                            {
                                await connectDevice(a.Device);
                            }
                        }

                    };

                    adapter.DeviceConnectionLost += (s, a) =>
                    {
                        Debug.WriteLine("Dispositivo desconectado: " + a.Device.Name + " ID: " + a.Device.Id);
                        Device.BeginInvokeOnMainThread(async () => {
                            await Application.Current.MainPage.DisplayAlert("Atención", "Se ha perdido la conexión con uno o varios dispositivos. Reintente de nuevo la conexión", "Aceptar");

                        });

                    };

                    adapter.ScanTimeoutElapsed += async (s, a) =>
                    {
                        if(!isAllConnectedDevices(lDevices))
                          await Application.Current.MainPage.DisplayAlert("Atención", "No se ha podido establecer conexión con el/los dispositivo/s. Compruebe que están disponibles y vuelva a intentarlo", "Aceptar");


                    };
                }
                catch (DeviceConnectionException er)
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Se ha producido un error al conectar con el dispositivo.", "Aceptar");
                    Debug.WriteLine("ERROR: " + er.Message);
                }
                catch (Exception er)
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Se ha producido un innesperado.", "Aceptar");
                    Debug.WriteLine("ERROR: " + er.Message);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Imposible conectar con el/los dispositivo/s. Por favor, active el bluetooth.", "Aceptar");
                Debug.WriteLine("BLE no disponible");
            }
        }

        public bool isAllConnectedDevices(List<DeviceModel> lDevicesActivity)
        {
            if (adapter.ConnectedDevices.Count() == lDevicesActivity.Count())
                return true;
            else
                return false;
        }
        private async Task connectDevice(IDevice deviceConnected)
        {
            Debug.WriteLine("Intentando conexion con: " + deviceConnected.Name);

            try
            {

                if (deviceConnected.State == DeviceState.Disconnected)
                {
                    await adapter.ConnectToDeviceAsync(deviceConnected);
                    lDevicesConnected.Add(deviceConnected);
                    Debug.WriteLine("Dispositivo conectado: " + deviceConnected.Name);

                    //Servicios y caracteristicas y descriptores
                    var services = await deviceConnected.GetServicesAsync();
                    IService customerService = null;
                    foreach (IService service in services)
                    {
                        if (service.Id.Equals(new Guid("0000ffe0-0000-1000-8000-00805f9b34fb")))
                        {
                            customerService = service;
                        }
                    }

                    var characteristics = await customerService.GetCharacteristicsAsync();
                    UnicodeEncoding uniencoding = new UnicodeEncoding();
                    byte[] one = uniencoding.GetBytes("0");
                    ICharacteristic characteristicRW = null;
                    //Buscamos la caracteristica que permite escribir, leer y actualizar
                    foreach (ICharacteristic characteristic in characteristics)
                    {
                        if (characteristic.CanRead && characteristic.CanWrite && characteristic.CanUpdate)
                        {
                            characteristicRW = characteristic;
                        }
                    }
                    await characteristicRW.WriteAsync(one);

                    characteristicRW.ValueUpdated += (s, a) =>
                    {
                        byte[] valueBytes = a.Characteristic.Value;
                        String data = string.Concat(valueBytes.Select(b => b.ToString("X2")));

                        DeviceData deviceData = new DeviceData();
                        deviceData.DeviceName = deviceConnected.Name;
                        deviceData.Data = data;
                        lDeviceData.Add(deviceData);

                        Debug.WriteLine(data, "Leyendo datos de " + deviceConnected.Name + ": ");
                    };
                    await characteristicRW.StartUpdatesAsync();

                    //var bytes = await characteristics[0].ReadAsync();
                }

            }
            catch (DeviceConnectionException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Se ha producido un error al conectar con el dispositivo " + deviceConnected.Name, "Aceptar");
                Debug.WriteLine("Error al conectar con el dispositivo: " + deviceConnected.Name + "." + ex);
            }
        }

        public static void saveData(SessionInit sessionInit)
        {
            Debug.WriteLine("Guardando datos.....");
            App.Database.SaveSessionInit(sessionInit);

            foreach (DeviceData deviceData in lDeviceData)
            {
                SessionData sessionData = new SessionData();
                sessionData.SessionInitId = sessionInit.ID;
                sessionData.DeviceName = deviceData.DeviceName;
                sessionData.Data = deviceData.Data;
                App.Database.SaveSessionData(sessionData);
            }
        }
    }
}
