using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using smartCubes.Models;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace smartCubes.Utils
{
    public class ConnectDevices
    {
        private DateTime datetime;
        private IAdapter adapter;
        private IBluetoothLE ble;
        private static List<DeviceData> lDeviceData;
        private List<IDevice> lDevicesConnected;


        public String StudentCode { get; set; }


        public ConnectDevices()
        {
            InitBLE();
        }

        private async Task InitBLE()
        {
            
            lDevicesConnected = new List<IDevice>();
            lDeviceData = new List<DeviceData>();

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            adapter.ScanTimeout = 10000;


            
            
            ble.StateChanged += async (s, e) =>
            {
                if (ble.State.Equals(BluetoothState.On))
                {
                    await SearchDevice();
                }
                else if(ble.State.Equals(BluetoothState.TurningOff))
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "El bluetooth del dispositivo ha sido apagado, por favor vuelva a encenderlo", "Aceptar");
                }

            };
            if (ble.State.Equals(BluetoothState.On))
            {
                await SearchDevice();
            }
        }

        private async Task SearchDevice()
        {

            if (ble.IsAvailable && ble.IsOn)
            {
                datetime = DateTime.Now;

                try
                {
                    adapter.DeviceDiscovered += async (s, a) =>
                    {
                        ActivityModel currentActivity = null;
                        try
                        {
                            currentActivity = (ActivityModel) await BlobCache.UserAccount.GetObject<ActivityModel>("currentActivity");
                        }
                        catch(Exception e)
                        {
                            Debug.WriteLine("Error al recuperar la actividad de la sesion: " + e.Message);
                        }
                       
                        Debug.WriteLine("Dispositivo encontrado: " + a.Device.Name + " ID: " + a.Device.Id + "Estado: " + a.Device.State);
                        foreach (DeviceModel device in currentActivity.Devices)
                        {
                            if (a.Device.Name != null && device.Name.Equals(a.Device.Name) && a.Device.State.Equals(DeviceState.Disconnected))
                            {
                                await ConnectDevice(a.Device, device.Service);
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
                        ActivityModel currentActivity = null;
                        try
                        {
                            currentActivity = (ActivityModel) await BlobCache.UserAccount.GetObject<ActivityModel>("currentActivity");
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("Error al recuperar la actividad de la sesion: " + e.Message);
                        }

                        if (!isAllConnectedDevices(currentActivity.Devices))
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
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }
        private async Task ConnectDevice(IDevice deviceConnected, string service)
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
                    foreach (IService iService in services)
                    {
                        if (iService.Id.Equals(new Guid(service)))
                        {
                            customerService = iService;
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
            catch (DeviceConnectionException ex )
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Se ha producido un error al conectar con el dispositivo " + deviceConnected.Name, "Aceptar");
                Debug.WriteLine("Error al conectar con el dispositivo: " + deviceConnected.Name + "." + ex);
            }
            catch (Exception ex )
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Se ha producido un error al conectar con los dispositivos ", "Aceptar");
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
