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
using Akavache;
using System.Reactive.Linq;
using System.Threading;
using Plugin.BLE.Abstractions.EventArgs;

namespace smartCubes.ViewModels.Session
{
    public class PlaySessionViewModel : BaseViewModel
    {
        private const string INICIAR = "Iniciar";
        private const string REANUDAR = "Reanudar";
        private const string DETENER = "Detener";
        private const string FINALIZAR = "Finalizar";
        private const string PAUSAR = "Pausar";


        private SessionModel session;
        private int intMilliseconds;
        private int intSeconds;
        private int intMinutes;
        private List<DeviceModel> lDevices;
        private SessionInit sessionInit;
        private IAdapter adapter;
        private INavigation Navigation;
        private CancellationToken token;
        private CancellationTokenSource source;

        private List<DeviceData> lDeviceData;
        private List<ICharacteristic> characteristics;
        private ActivityModel currentActivity;

        public PlaySessionViewModel(SessionModel session, INavigation navigation)
        {
            this.session = session;
            Navigation = navigation;
            sessionInit = new SessionInit();
            sessionInit.SessionId = session.ID;
            sessionInit.Date = DateTime.Now;

            Loading = true;
            ColorFrame = "#e6475f";

            lDeviceData = new List<DeviceData>();
            characteristics = new List<ICharacteristic>();

            /*source = new CancellationTokenSource();
            token.WaitHandle.WaitOne(TimeSpan.FromSeconds(5));
            token = source.Token;*/

            Title = session.Name;
            ActivityName = session.ActivityName;

            ResetChronometer();
            Task.Run(() =>
            {
                currentActivity = Json.GetActivityByName(session.ActivityName);
                BlobCache.UserAccount.InsertObject("currentActivity", currentActivity);
                lDevices = new List<DeviceModel>(currentActivity.Devices);
            });

            adapter = CrossBluetoothLE.Current.Adapter;

            /*  Device.BeginInvokeOnMainThread(async () =>
              {
                  await InitBLEConexion(token);
              });*/
        }


        private bool _StudentCodeEntry;
        public bool StudentCodeEntry
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

        private string _StudentCode;
        public string StudentCode
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

        private string _ColorFrame;
        public string ColorFrame
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

        private string _Minutes;
        public string Minutes
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

        private string _Seconds;
        public string Seconds
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

        private string _Milliseconds;
        public string Milliseconds
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

        private string _StartStop;
        public string StartStop
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
        private string _ActivityName;
        public string ActivityName
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

            if (!IsAllConnectedDevices(lDevices))
            {
               await Application.Current.MainPage.DisplayAlert("Atención", "Hay dispositivos que no están conectados. Por favor, pulse el botón reconnectar", "Aceptar");
               return;
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
                    intMilliseconds += 10;

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
                var action = await Application.Current.MainPage.DisplayActionSheet("¿Que desea hacer?", PAUSAR, FINALIZAR);

                if (action.Equals(FINALIZAR))
                {
                    sessionInit.Time = Minutes + ":" + Seconds + ":" + Milliseconds;
                    ResetChronometer();
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
        private ICommand _OnTapGestureRecognizerTappedReconnect;
        public ICommand OnTapGestureRecognizerTappedReconnect
        {
            get { return _OnTapGestureRecognizerTappedReconnect ?? (_OnTapGestureRecognizerTappedReconnect = new Command(() => OnTapGestureRecognizerTappedReconnectExecute())); }
        }

        private async void OnTapGestureRecognizerTappedReconnectExecute()
        {
            if (!IsAllConnectedDevices(lDevices))
            {
                var answer = await Application.Current.MainPage.DisplayAlert("Atención", "¿Desea volver a intentar conectar con los dispositivos? ", "Si", "No");
                if (answer)
                {
                    Loading = true;
                    source = new CancellationTokenSource();
                    token = source.Token;
                    await Connect(token);
                }

                Loading = false;
            }
            else if (IsAllConnectedDevices(lDevices))
            {
                await Application.Current.MainPage.DisplayAlert("Información", "Todos los dispositivos están conectados", "Aceptar");
            }
        }
        private ICommand _OnTapGestureRecognizerTappedClose;
        public ICommand OnTapGestureRecognizerTappedClose
        {
            get { return _OnTapGestureRecognizerTappedClose ?? (_OnTapGestureRecognizerTappedClose = new Command(() => OnTapGestureRecognizerTappedCloseExecute())); }
        }

        private async void OnTapGestureRecognizerTappedCloseExecute()
        {
            if (!StartStop.Equals(INICIAR))
            {
                StartStop = REANUDAR;
                var answer = await Application.Current.MainPage.DisplayAlert("Atención", " La actividad en curso no será guardada. ¿Está seguro que desea salir?", "Si", "No");

                if (answer)
                {
                    await Navigation.PopModalAsync();
                }
            }
            else
            { 
                var answer = await Application.Current.MainPage.DisplayAlert("Atención", "¿Está seguro que desea salir de la sesión?", "Si", "No");

                if (answer)
                {
                    await Navigation.PopModalAsync();
                }
                StartStop = INICIAR;
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

        /*___________________________________________________________________________________________
         * 
         * 
         ************************************ CONEXIÓN BLE ******************************************
         * 
         * _________________________________________________________________________________________*/

        public async Task InitBLEConexion(CancellationTokenSource source)
        {
            this.source = source;
            token = this.source.Token;
            token.WaitHandle.WaitOne(TimeSpan.FromSeconds(5));

            IBluetoothLE ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            adapter.ScanTimeout = 10000;

            ble.StateChanged += async (s, e) =>
            {
                if (ble.State.Equals(BluetoothState.On))
                {
                    await Connect(token);
                }
                else if (ble.State.Equals(BluetoothState.TurningOff))
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "El bluetooth del dispositivo ha sido apagado, por favor vuelva a encenderlo", "Aceptar");
                }

            };
            if (ble.State.Equals(BluetoothState.On))
            {
                await Connect(token);
            }
        }
        private async Task Connect(CancellationToken token)
        {
            await SubscribeEvents();

            foreach (DeviceModel device in currentActivity.Devices)
            {
                await Task.Run(async () =>
                 {
                     await ConnectDevice(device, token);
                 });
            }
        }
        private async Task ConnectDevice(DeviceModel device, CancellationToken cancellationToken)
        {
            try
            {
                if (adapter.ConnectedDevices.ToList().Find(d => d.Id != null && d.Id.Equals(device.Uuid)) == null)
                {
                    var connectedDevice = await adapter.ConnectToKnownDeviceAsync(Guid.Parse(device.Uuid), new ConnectParameters(false,false), cancellationToken);
                    var service = await connectedDevice.GetServiceAsync(Guid.Parse(device.Service));
                    var characteristic = await service.GetCharacteristicAsync(Guid.Parse(device.Characteristic));

                    UnicodeEncoding uniencoding = new UnicodeEncoding();
                    byte[] zero = uniencoding.GetBytes("0");
                    Device.BeginInvokeOnMainThread(() =>
                    {
                         characteristic.WriteAsync(zero);
                    });

                    characteristic.ValueUpdated += (s, a) =>
                    {
                        byte[] valueBytes = a.Characteristic.Value;

                        string data = string.Concat(valueBytes.Select(b => b.ToString("X2")));

                        DeviceData deviceData = new DeviceData();
                        deviceData.DeviceName = connectedDevice.Name;
                        deviceData.Data = valueBytes;
                        lDeviceData.Add(deviceData);

                        Debug.WriteLine(data, "Leyendo datos de " + connectedDevice.Name + ": ");
                    };
                    characteristics.Add(characteristic);

                    await characteristic.StartUpdatesAsync();
                }
            }
            catch (DeviceConnectionException e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "No es posible conectar con el dispositivo " + device.Name, "Aceptar");
                    Debug.WriteLine("Error al conectar con el dispositivo: " + device.Name + "." + e);
                    Loading = false;
                });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("Las tareas han sido canceladas");
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Se ha producido un error innesperado", "Aceptar");
                    Debug.WriteLine("Error al conectar con el dispositivo: " + device.Name + "." + e);
                    Loading = false;
                });
            }
            
        }
        private async Task SubscribeEvents()
        {
            try
            {
                adapter.DeviceConnected += DeviceConnectedEvent;
                adapter.DeviceConnectionLost += LostDeviceEvent;
            }
            catch (Exception er)
            {
                await DisconnectAll();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Se ha producido un innesperado.", "Aceptar");
                });
                Debug.WriteLine("ERROR: " + er.Message);
            }
        }

        private void LostDeviceEvent(object sender, DeviceErrorEventArgs e)
        {
            if (!StartStop.Equals(INICIAR))
            {
                StartStop = REANUDAR;
            }
            else
            {
                StartStop = INICIAR;
            }

            ColorFrame = "#e6475f";
            Debug.WriteLine("Dispositivo desconectado: " + e.Device.Name + " ID: " + e.Device.Id);

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Se ha perdido la conexión con el dispositivo " + e.Device.Name + ". Reintente de nuevo la conexión", "Aceptar");
                await DisconnectAll();
            });
        }
        private void DeviceConnectedEvent(object sender, EventArgs e)
        {
            if (IsAllConnectedDevices(lDevices))
            {
                ColorFrame = "#3faf83";
                Loading = false;
            }
        }
      
        public async Task DisconnectAll()
        {
            foreach (IDevice device in adapter.ConnectedDevices)
            {
                if (device.State == DeviceState.Connected && lDevices.Find(d => d.Uuid.Equals(device.Id.ToString())) != null)
                {
                    string serviceDevice = currentActivity.Devices.Find(d => d.Uuid.Equals(device.Id.ToString())).Service;

                    var services = await device.GetServicesAsync();
                    IService customerService = null;
                    foreach (IService service in services)
                    {
                       if (service.Id.Equals(new Guid(serviceDevice)))
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
                    await CrossBluetoothLE.Current.Adapter.DisconnectDeviceAsync(device);
                }
            }
            characteristics = new List<ICharacteristic>();
            adapter.DeviceConnectionLost -= LostDeviceEvent;
            adapter.DeviceConnected -= DeviceConnectedEvent;

            source.Cancel();
        }

        public void WriteDevices(String number)
        {
            try { 
                if (characteristics != null && characteristics.Count > 0)
                {
                        UnicodeEncoding uniencoding = new UnicodeEncoding();
                        byte[] one = uniencoding.GetBytes(number);
                        //Buscamos la caracteristica que permite escribir, leer y actualizar
                        foreach (ICharacteristic characteristic in characteristics)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                characteristic.WriteAsync(one);
                            });
                        }
                 }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar el código de alumno", "Aceptar");
                    });
                }
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Error no controlado", "Aceptar");
                });
                Debug.WriteLine("Error al conectar con el dispositivo." + e);
            }
        }

        public bool IsAllConnectedDevices(List<DeviceModel> lDevicesActivity)
        {
            if (adapter.ConnectedDevices.Count() == lDevicesActivity.Count())
            {
                Loading = false;
                return true;
            }
            else
            {
                return false;
            }             
        }
    }
}