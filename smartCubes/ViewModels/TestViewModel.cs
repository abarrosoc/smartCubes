using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using smartCubes.Models;
using Xamarin.Forms;

namespace smartCubes.ViewModels
{
    public class ActivitiesViewModel : BaseViewModel
    {
        public ActivitiesViewModel()
        {

        }

        private string _Test_label;

        public string Test_label
        {
            get
            {
                return _Test_label;
            }
            set
            {
                _Test_label = value;
                RaisePropertyChanged();
            }
        }
        #region SearchByName Command

        private ICommand _SearchByName;

        public ICommand SearchByName
        {
            get
            {
                return _SearchByName ?? (_SearchByName = new Command(
                    async () => await ExecuteSearchByNameCommand()));
            }
        }
        public async Task ExecuteSearchByNameCommand(){
            Debug.WriteLine("Command!");
            await Task.Delay(1000);
            Test_label = "HOLLLLLAAAA";
        }
        #endregion
    }
}

