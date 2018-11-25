using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using smartCubes.Models;

namespace smartCubes.Utils
{
    public class ConnectDevices
    {
        private static DateTime datetime;
        private static IAdapter adapter;
        private static IBluetoothLE ble;

        public ConnectDevices()
        {
            //SearchDevice(lDevices);
        }

        public static async Task Start(List<DeviceModel> lDevices)
        {

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;

            if (!ble.State.Equals(BluetoothState.On))
            {
                ble.StateChanged += async (s, e) =>
                {
                    Debug.WriteLine($"The bluetooth state changed to {e.NewState}");
                    await SearchDevice(lDevices);
                };
            }
            else
            {
                await SearchDevice(lDevices);
            }
        }

        private static async Task SearchDevice(List<DeviceModel> lDevices)
        {

            if (ble.IsAvailable && ble.IsOn)
            {
                Debug.WriteLine("Start Scanning...");
                datetime = DateTime.Now;
                try
                {
                    adapter.DeviceDiscovered += (s, a) =>
                    {
                        //Debug.WriteLine("Dispositivo encontrado: " + a.Device.Name + " ID: " + a.Device.Id);
                        foreach (DeviceModel device in lDevices)
                        {
                            if (device.Uuid.Equals(a.Device.Id.ToString()))
                            {
                                Debug.WriteLine("Add new device: " + a.Device.Name + " ID: " + a.Device.Id);
                                connectDevice(a.Device);
                            }
                        }
                    };
                    await adapter.StartScanningForDevicesAsync();
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
                    Debug.WriteLine("El dispositivo ya esta conectado");
                }

            }
            catch (DeviceConnectionException ex)
            {
                Debug.WriteLine("Error al conectar con el dispositivo: " + deviceConnected.Name);
            }
        }
    }
}
