using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.Utils;
using Xamarin.Forms;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System.Text;
using System.Linq;

namespace smartCubes.ViewModels.Session
{
    public class PlaySessionViewModel : BaseViewModel
    {
        private const String INICIAR = "Iniciar";
        private const String REANUDAR = "Reanudar";
        private const String DETENER = "Detener";
        private const String FINALIZAR = "Finalizar";
        private const String PAUSAR = "Pausar";

        private SessionModel session;
        private int intMilliseconds;
        private int intSeconds;
        private int intMinutes;
        private List<DeviceModel> lDevices;
        private SessionInit sessionInit;

        private DateTime datetime;
        private IAdapter adapter;
        private IBluetoothLE ble;

        private List<DeviceData> lDeviceData;
        private List<IDevice> lDevicesConnected;


        public PlaySessionViewModel(SessionModel session)
        {
            this.session = session;
            sessionInit = new SessionInit();
            sessionInit.SessionId = session.ID;
            sessionInit.Date = DateTime.Now;
            ColorFrame = "Red";
            ReconnectEnable = true;

            Title = session.Name;
            ActivityName = session.ActivityName;
            ResetChronometer();


            lDevices = new List<DeviceModel>();

            ActivityModel activity = Json.getActivityByName(session.ActivityName);
            lDevices = activity.Devices;

            Start(lDevices);


        }
        private Boolean _StudentCodeEntry;

        public Boolean StudentCodeEntry
        {
            get
            {
                return _StudentCodeEntry;
            }
            set
            {
                _StudentCodeEntry = value;
                RaisePropertyChanged();
            }
        }

        private String _StudentCode;

        public String StudentCode
        {
            get
            {
                return _StudentCode;
            }
            set
            {
                _StudentCode = value;
                RaisePropertyChanged();
            }
        }

        private String _ColorFrame;

        public String ColorFrame
        {
            get
            {
                return _ColorFrame;
            }
            set
            {
                _ColorFrame = value;
                RaisePropertyChanged();
            }
        }

        private String _Minutes;

        public String Minutes
        {
            get
            {
                return _Minutes;
            }
            set
            {
                _Minutes = value;
                RaisePropertyChanged();
            }
        }

        private String _Seconds;

        public String Seconds
        {
            get
            {
                return _Seconds;
            }
            set
            {
                _Seconds = value;
                RaisePropertyChanged();
            }
        }

        private String _Milliseconds;

        public String Milliseconds
        {
            get
            {
                return _Milliseconds;
            }
            set
            {
                _Milliseconds = value;
                RaisePropertyChanged();
            }
        }

        private String _StartStop;

        public String StartStop
        {
            get
            {
                return _StartStop;
            }
            set
            {
                _StartStop = value;
                RaisePropertyChanged();
            }
        }
        private String _ActivityName;

        public String ActivityName
        {
            get
            {
                return _ActivityName;
            }
            set
            {
                _ActivityName = value;
                RaisePropertyChanged();
            }
        }
        private bool _ReconnectEnable;

        public bool ReconnectEnable
        {
            get
            {
                return _ReconnectEnable;
            }
            set
            {
                _ReconnectEnable = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _timerCommand;
        public ICommand TimerCommand
        {
            get { return _timerCommand ?? (_timerCommand = new Command(() => TimerCommandExecute())); }
        }



        private async void TimerCommandExecute()
        {
            if (String.IsNullOrEmpty(StudentCode))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar el código de alumno", "OK");
                return;
            }

            if (!isAllConnectedDevices(lDevices))
            {
                var answer = await Application.Current.MainPage.DisplayAlert("Atención", "No se ha podido establecer conexión con todos los dispositivos. ¿Desea intentarlo de nuevo?", "Reintentar", "Cancelar");
                if (answer)
                {
                    await Start(lDevices);
                    if (isAllConnectedDevices(lDevices))
                        ColorFrame = "Green";
                    else
                        ColorFrame = "Red";

                    return;
                }
            }

            sessionInit = new SessionInit();
            sessionInit.SessionId = session.ID;
            sessionInit.StudentCode = StudentCode;
            sessionInit.Date = DateTime.Now;


            StudentCode = StudentCode;
            if (StartStop.Equals(INICIAR) || StartStop.Equals(REANUDAR))
            {
                StudentCodeEntry = false;
                StartStop = DETENER;
            }
            else
            {
                StudentCodeEntry = true;
                StartStop = INICIAR;
            }
            WriteDevices("1");
            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {
                if (StartStop.Equals(INICIAR) || StartStop.Equals(REANUDAR))
                {
                    WriteDevices("0");
                    return false;
                }
                else
                {
                    intMilliseconds = intMilliseconds + 10;

                    if (intMilliseconds == 1000)
                    {
                        intMilliseconds = 0;
                        intSeconds++;
                    }

                    if (intSeconds == 60)
                    {
                        intSeconds = 0;
                        intMinutes++;
                    }

                    if (intMinutes / 10 == 0)
                        Minutes = "0" + intMinutes.ToString();
                    else
                        Minutes = intMinutes.ToString();

                    if (intSeconds / 10 == 0)
                        Seconds = "0" + intSeconds.ToString();
                    else
                        Seconds = intSeconds.ToString();

                    if (intMilliseconds / 100 == 0)
                        Milliseconds = "0" + (intMilliseconds / 10).ToString();
                    else
                        Milliseconds = (intMilliseconds / 10).ToString();

                    return true;
                }
            });

            if (StartStop.Equals(INICIAR))
            {
                StartStop = REANUDAR;
                var action = await Application.Current.MainPage.DisplayActionSheet("¿Que desea hacer?", "Cancelar", FINALIZAR, PAUSAR);

                if (action.Equals(FINALIZAR))
                {
                    sessionInit.Time = Minutes + ":" + Seconds + ":" + Milliseconds;
                    ResetChronometer();
                    saveData(sessionInit);

                }
            }
        }
        private ICommand _reconnectCommand;
        public ICommand ReconnectCommand
        {
            get { return _reconnectCommand ?? (_reconnectCommand = new Command(() => ReconnectCommandExecute())); }
        }



        private async void ReconnectCommandExecute()
        {
            adapter = CrossBluetoothLE.Current.Adapter;
            if(!adapter.IsScanning && !isAllConnectedDevices(lDevices)){
                var answer = await Application.Current.MainPage.DisplayAlert("Atención", "¿Desea volver a intentar conectar con los dispositivos?", "Si", "No");
                if (answer)
                    Start(lDevices);
            }
        }

        private void ResetChronometer()
        {
            StartStop = INICIAR;
            Seconds = "00";
            Milliseconds = "00";
            Minutes = "00";
            intMilliseconds = 0;
            intMinutes = 0;
            intSeconds = 0;
            StudentCodeEntry = true;
            StudentCode = null;
        }

        /*
         * 
         * 
         * 
         * 
         * CONEXION DISPOSITIVOS BLE
         * 
         * 
         * 
         * 
         */

        public async Task Start(List<DeviceModel> lDev)
        {
            lDevicesConnected = new List<IDevice>();
            lDeviceData = new List<DeviceData>();
            lDevices = new List<DeviceModel>();
            lDevices = lDev;

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            adapter.ScanTimeout = 30000;
 

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
                Debug.WriteLine("Start Scanning...");
                datetime = DateTime.Now;

                try
                {
                    adapter.ScanMode=ScanMode.Balanced;

                    adapter.DeviceDiscovered += async (s, a) =>
                    {
                        Debug.WriteLine("Dispositivo encontrado: " + a.Device.Name + " ID: " + a.Device.Id + "Estado: " + a.Device.State);
                        foreach (DeviceModel device in lDevices)
                        {
                            if (a.Device.Name != null && device.Name.Equals(a.Device.Name) && a.Device.State.Equals(DeviceState.Disconnected))
                            {
                                Debug.WriteLine("Nuevo dispositivo: " + a.Device.Name + " ID: " + a.Device.Id);
                                await connectDevice(a.Device);
                            }
                        }

                    };
                    adapter.DeviceConnected += (s, a) =>
                    {
                        if (isAllConnectedDevices(lDevices))
                        {
                            ColorFrame = "Green";
                            ReconnectEnable = false;
                        }
                    };

                        adapter.DeviceConnectionLost += async (s, a) =>
                        {
                            if (!StartStop.Equals(INICIAR))
                                StartStop = REANUDAR;
                            else
                                StartStop = INICIAR;
                        
                            ReconnectEnable = true;
                            Debug.WriteLine("Dispositivo desconectado: " + a.Device.Name + " ID: " + a.Device.Id);
                            ColorFrame = "Red";
                            var answer = await Application.Current.MainPage.DisplayAlert("Atención", "Se ha perdido la conexión con uno o varios dispositivos. ¿Desea volver a conectar?", "Conectar", "Cancelar");
                            if (answer)
                                await Start(lDevices);

                        };
                    if (!ble.Adapter.IsScanning)
                    {
                        await adapter.StartScanningForDevicesAsync();
                    }
                    

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

        private async Task connectDevice(IDevice deviceConnected)
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
                    lDevicesConnected.Add(deviceConnected);
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
        public async void disconnectAll()
        {

            foreach (IDevice device in adapter.ConnectedDevices)
            {
                if (device.State == DeviceState.Connected)
                    await adapter.DisconnectDeviceAsync(device);
            }
            await adapter.StopScanningForDevicesAsync();

           // adapter = null;
        }

        public async void WriteDevices(String number)
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
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar el código de alumno", "Aceptar");
            }
        }

        public bool isAllConnectedDevices(List<DeviceModel> lDevicesActivity)
        {
            if (adapter.ConnectedDevices.Count() == lDevicesActivity.Count())
                return true;
            else
                return false;
        }

        public void saveData(SessionInit sessionInit)
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
