using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace smartCubes.Utils
{
    public class Chronometer
    {

        public Chronometer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Debug.Write("TIMERR!");

              return true;
            });
        }

       
    }
}
