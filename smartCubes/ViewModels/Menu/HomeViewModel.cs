using System;
using System.Diagnostics;
using System.Windows.Input;
using smartCubes.View;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Menu
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand Click_Activity { get; private set; }

        public HomeViewModel()
        {
            Click_Activity = new Command(Click);
        }

        private void Click()
        {
            Debug.WriteLine("Click en actividad: "+ Title);
        }
    }
}

