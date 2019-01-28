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
        private static DateTime datetime;
        private static IAdapter adapter;
        private static IBluetoothLE ble;
        private static List<DeviceModel> lDevices;
        private static List<DeviceData> lDeviceData;
        private static List<IDevice> lDevicesConnected;

        public static String StudentCode { get; set; }


        public ConnectDevices(List<DeviceModel> lDev,PlaySessionViewModel playSessionViewModel)
        {
            Start(lDev,playSessionViewModel);
        }

        public async void Start(List<DeviceModel> lDev,PlaySessionViewModel playSessionViewModel)
        {
            lDeviceData = new List<DeviceData>();
            lDevices = new List<DeviceModel>();
            lDevices = lDev;
            lDevicesConnected = new List<IDevice>();
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;


            if (!ble.State.Equals(BluetoothState.On))
            {
                ble.StateChanged += async (s, e) =>
                {
                    Debug.WriteLine("BLE state changed to {e.NewState}");
                    SearchDevice(lDevices, playSessionViewModel);
                };
            }
            else
            {
                SearchDevice(lDevices, playSessionViewModel);
            }
        }

        private async void SearchDevice(List<DeviceModel> lDevices,PlaySessionViewModel playSessionViewModel)
        {

            if (ble.IsAvailable && ble.IsOn)
            {
                Debug.WriteLine("Start Scanning...");
                datetime = DateTime.Now;
               
                try
                {
                    adapter.DeviceDiscovered += (s, a) =>
                    {
                        Debug.WriteLine("Dispositivo encontrado: " + a.Device.Name + " ID: " + a.Device.Id);
                        foreach (DeviceModel device in lDevices)
                        {
                            if (a.Device.Name!=null && device.Name.Equals(a.Device.Name))
                            {
                                Debug.WriteLine("Nuevo dispositivo: " + a.Device.Name + " ID: " + a.Device.Id);
                                connectDevice(a.Device);
                                lDevicesConnected.Add(a.Device);

                                if (lDevicesConnected.Count() == lDevices.Count())
                                    playSessionViewModel.ChangeFrameColor();
                                
                            }
                        }

                    };
                    await adapter.StartScanningForDevicesAsync();

                }
                catch (DeviceConnectionException  er)
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

        private static async void connectDevice(IDevice deviceConnected)
        {
            Debug.WriteLine("Intentando conexion con: " + deviceConnected.Name);

            try
            {
                //Dispositivos seleccionado
                //var deviceConnected = device as IDevice;
                //Conectamos con el dispositivo

                if (deviceConnected.State == DeviceState.Disconnected)
                {
                    await adapter.ConnectToDeviceAsync(deviceConnected);
                    //Mensaje en pantalla
                    //await DisplayAlert(deviceConnected.Name, "Estado:" + deviceConnected.State, "OK");
                    Debug.WriteLine("Dispositivo conectado: " + deviceConnected.Name);
                    //Servicios y caracteristicas y descriptores
                    var services = await deviceConnected.GetServicesAsync();
                    var characteristics = await services[0].GetCharacteristicsAsync();

                    UnicodeEncoding uniencoding = new UnicodeEncoding();
                    byte[] one = uniencoding.GetBytes("0");
                    await characteristics[0].WriteAsync(one);
                    //lectura de datos
                    characteristics[0].ValueUpdated += (s, a) =>
                    {
                        byte[] valueBytes = a.Characteristic.Value;
                        //await characteristics[0].ReadAsync(); //lee constantemente
                        String data = string.Concat(valueBytes.Select(b => b.ToString("X2")));

                        DeviceData deviceData = new DeviceData();
                        deviceData.DeviceName = deviceConnected.Name;
                        deviceData.Data = data;
                        lDeviceData.Add(deviceData);

                        Debug.WriteLine(data, "Leyendo datos de " + deviceConnected.Name + ": ");
                    };
                    await characteristics[0].StartUpdatesAsync();

                    //var bytes = await characteristics[0].ReadAsync();
                }
                else
                {
                    //Si esta conectado, se desconecta
                    Debug.WriteLine("El dispositivo ya esta conectado");
                }

            }
            catch (DeviceConnectionException ex)
            {
                Debug.WriteLine("Error al conectar con el dispositivo: " + deviceConnected.Name + "." + ex);
            }
        }
        public static async void disconnectAll(PlaySessionViewModel playSessionViewModel)
        {
            foreach (IDevice device in lDevicesConnected)
            {
                if (device.State == DeviceState.Connected)
                    await adapter.DisconnectDeviceAsync(device);
            }
            lDevicesConnected = new List<IDevice>();
            playSessionViewModel.ColorFrame = "Red";

        }
        public static async void WriteDevices(String number)
        {

            if (lDevices != null)
            {
                foreach (IDevice device in adapter.ConnectedDevices)
                {
                    var services = await device.GetServicesAsync();
                    var characteristics = await services[0].GetCharacteristicsAsync();

                    UnicodeEncoding uniencoding = new UnicodeEncoding();
                    byte[] one = uniencoding.GetBytes(number);
                    await characteristics[0].WriteAsync(one);
                }
            }else{
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar el código de alumno", "Aceptar");
            }
        }

        public static bool isAllConnectedDevices(List<DeviceModel> lDevicesActivity)
        {
            if (adapter.ConnectedDevices.Count() == lDevicesActivity.Count())
                return true;
            else
                return false;
        }

        public static void saveData(SessionInit sessionInit){
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
