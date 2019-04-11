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
    public class PlaySessionViewModel2 : BaseViewModel
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


        public PlaySessionViewModel2(SessionModel session)
        {
            this.session = session;
            sessionInit = new SessionInit();
            sessionInit.SessionId = session.ID;
            sessionInit.Date = DateTime.Now;

            Loading = true;
            IsEnabledPage = false;
            ColorFrame = "Red";

            lDeviceData = new List<DeviceData>();

          

            Title = session.Name;
            ActivityName = session.ActivityName;

            ResetChronometer();

            ActivityModel activity = Json.getActivityByName(session.ActivityName);
            lDevices = new List<DeviceModel>(activity.Devices);

            adapter = CrossBluetoothLE.Current.Adapter;
            CrossBluetoothLE.Current.Adapter.StartScanningForDevicesAsync();

            if (isAllConnectedDevices(lDevices))
            {
                ColorFrame = "Green";
                Loading = false;
                IsEnabledPage = true;
            }
            else
            {
                ColorFrame = "Red";
            }
               
            SearchDevice();

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

        private bool _Loading;

        public bool Loading
        {
            get
            {
                return _Loading;
            }
            set
            {
                _Loading = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsEnabledPage = false;

        public bool IsEnabledPage
        {
            get
            {
                return _IsEnabledPage;
            }
            set
            {
                _IsEnabledPage = value;
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
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar el código de alumno", "Aceptar");
                return;
            }

            if (!isAllConnectedDevices(lDevices))
            {
                Loading = true;
                IsEnabledPage = false;
                var answer = await Application.Current.MainPage.DisplayAlert("Atención", "No se ha podido establecer conexión con todos los dispositivos. ¿Desea intentarlo de nuevo?", "Reintentar", "Cancelar");
                if (answer)
                {
                  
                    await CrossBluetoothLE.Current.Adapter.StartScanningForDevicesAsync();

                    if (isAllConnectedDevices(lDevices))
                    {
                        ColorFrame = "Green";
                        Loading = false;
                        IsEnabledPage = true;
                    }
                    else
                    {
                        ColorFrame = "Red";
                    }

                    return;
                }
                else{
                    Loading = false;
                    IsEnabledPage = true;
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
                    ConnectDevices.saveData(sessionInit);
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
            Loading = true;
            IsEnabledPage = false;
            if (!adapter.IsScanning && !isAllConnectedDevices(lDevices))
            {
                var answer = await Application.Current.MainPage.DisplayAlert("Atención", "¿Desea volver a intentar conectar con los dispositivos?", "Si", "No");
                if (answer)
                {
                    await CrossBluetoothLE.Current.Adapter.StartScanningForDevicesAsync();    
                }
                else
                {
                    Loading = false;  
                    IsEnabledPage = true;
                }    
            }
            else if(isAllConnectedDevices(lDevices)){
                await Application.Current.MainPage.DisplayAlert("Información", "Todos los dispositivos están conectados", "Aceptar");
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

        private async Task SearchDevice()
        {
            try
            {
                adapter.DeviceConnected += (s, a) =>
                {
                    if (isAllConnectedDevices(lDevices))
                    {
                        ColorFrame = "Green";
                        Loading = false;
                        IsEnabledPage = true;
                    }
                };

                adapter.DeviceConnectionLost += (s, a) =>
                    {
   
                    if (!StartStop.Equals(INICIAR))
                    {
                        StartStop = REANUDAR;
                    }   
                    else
                    {
                        StartStop = INICIAR;
                    }
 
                        ColorFrame = "Red";

                    };
                adapter.ScanTimeoutElapsed += (s, a) =>
                {
                    Loading = false;
                    IsEnabledPage = true;
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




        public async Task DisconnectAll()
        {
            await adapter.StopScanningForDevicesAsync();



            foreach (IDevice device in adapter.ConnectedDevices)
            {
                if (device.State == DeviceState.Connected)
                {
                    var services = await device.GetServicesAsync();
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

                    //Buscamos la caracteristica que permite escribir, leer y actualizar
                    foreach (ICharacteristic characteristic in characteristics)
                    {
                        if (characteristic.CanRead && characteristic.CanWrite && characteristic.CanUpdate)
                        {
                            characteristic.ValueUpdated -= null;
                        }
                    }

                    adapter.DeviceConnectionLost -= null;
                    await CrossBluetoothLE.Current.Adapter.DisconnectDeviceAsync(device);
                }
            }


        }


        public async void WriteDevices(String number)
        {

            if (lDevices != null)
            {
                foreach (IDevice device in adapter.ConnectedDevices)
                {
                    var services = await device.GetServicesAsync();
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
                    byte[] one = uniencoding.GetBytes(number);
                    //Buscamos la caracteristica que permite escribir, leer y actualizar
                    foreach (ICharacteristic characteristic in characteristics)
                    {
                        if (characteristic.CanRead && characteristic.CanWrite && characteristic.CanUpdate)
                        {
                            await characteristic.WriteAsync(one);
                        }
                    }
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



    }
}
