using System;
using System.Diagnostics;
using System.Windows.Input;
using smartCubes.Utils;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Session
{
    public class PlaySessionViewModel : BaseViewModel
    {
        private int Milliseconds;
        private int Seconds;
        private int Minutes;

        public PlaySessionViewModel()
        {
            Timer = "00:00:00";
            StartStop = "Iniciar";
            Seconds = 0;
            Milliseconds = 0;
            Minutes = 0;
        }

        private String _Timer;

        public String Timer
        {
            get
            {
                return _Timer;
            }
            set
            {
                _Timer = value;
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

        private ICommand _timerCommand;
        public ICommand TimerCommand
        {
            get { return _timerCommand ?? (_timerCommand = new Command(() => SaveCommandExecute())); }
        }

        private void SaveCommandExecute()
        {
            if (StartStop.Equals("Iniciar"))
                StartStop = "Detener";
    
            else
                StartStop = "Iniciar";
            
            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {
                Debug.Write("TIMERR!");
                if (StartStop.Equals("Iniciar"))
                    return false;

                else
                {
                    if (Milliseconds == 1000)
                    {
                        Milliseconds = 0;
                        Seconds++;
                    }
                    else{
                        Milliseconds=Milliseconds+10;
                    }

                    Timer = "00:" + Seconds.ToString() + ":" + (Milliseconds/10).ToString();
                    return true;
                }
            });
           
            



        }
    }
}
