﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.Utils;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Session
{
    public class PlaySessionViewModel : BaseViewModel
    {
        private int intMilliseconds;
        private int intSeconds;
        private int intMinutes;

        private List<DeviceModel> lDevices;

        public PlaySessionViewModel(SessionModel session)
        {
            Title = session.Name;
            ActivityName = session.ActivityName;
            StartStop = "Iniciar";
            Seconds = "00";
            Milliseconds = "00";
            Minutes = "00";
            StudentCodeEntry = true;

            lDevices = new List<DeviceModel>();
            ActivitiesModel activities = Json.getActivities();
            ActivityModel activity1 = activities.Activities[0];
            lDevices.Add(activity1.Devices[0]);
            lDevices.Add(activity1.Devices[1]);

            ConnectDevices.Start(lDevices);

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
        private ICommand _timerCommand;
        public ICommand TimerCommand
        {
            get { return _timerCommand ?? (_timerCommand = new Command(() => SaveCommandExecute())); }
        }

        private async void SaveCommandExecute()
        {
            if (!ConnectDevices.isAllConnectedDevices(lDevices))
            {
                var action = await Application.Current.MainPage.DisplayAlert("Atención", "No se ha podido establecer conexión con todos los dispositivos. ¿Desea intentarlo de nuevo?", "Reintentar", "Cancelar");
                if (action.ToString().Equals("True"))
                {
                    await ConnectDevices.Start(lDevices);
                    SaveCommandExecute();
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(StudentCode))
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar el código de alumno", "OK");
                    return;
                }
                else
                {
                    if (StartStop.Equals("Iniciar"))
                    {
                        StudentCodeEntry = false;
                        StartStop = "Detener";
                    }
                    else
                    {
                        StudentCodeEntry = true;
                        StartStop = "Iniciar";
                    }
                    ConnectDevices.WriteDevices("1");
                    Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                    {
                        if (StartStop.Equals("Iniciar")){
                            ConnectDevices.WriteDevices("0");
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

                    if (StartStop.Equals("Iniciar"))
                    {
                        var action = await Application.Current.MainPage.DisplayActionSheet("¿Que desea hacer?", "Cancelar", "Finalizar", "Pausar");
                        ConnectDevices.WriteDevices("0");
                        if (action.Equals("Finalizar"))
                        {
                            Debug.WriteLine("Guardar datos");
                            Seconds = "00";
                            Milliseconds = "00";
                            Minutes = "00";
                            intMilliseconds = 0;
                            intMinutes = 0;
                            intSeconds = 0;
                            StudentCodeEntry = true;
                        }
                        //OPCION REANUDAR???
                    }
                }
            }
        }
    }
}
