using System.Collections.ObjectModel;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Login;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Configuration
{
    public class ConfigurationViewModel:BaseViewModel
    {
        public ConfigurationViewModel()
        {
            lSettings = new ObservableCollection<SettingModel>();
            Title = "Ajustes";
        }

        private ObservableCollection<SettingModel> _lSettings;

        public  ObservableCollection<SettingModel> lSettings
        {
            get
            {
                return _lSettings;
            }
            set
            {
                _lSettings = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectItem;

        public string SelectItem
        {
            get
            {
                return _SelectItem;
            }
            set
            {
                _SelectItem = value;
                RaisePropertyChanged();
            }
        }


        private ICommand _ResetCommand;
        public ICommand ResetCommand
        {
            get { return _ResetCommand ?? (_ResetCommand = new Command(() => ResetCommandExecute())); }
        }
        private async void ResetCommandExecute()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Restaurar aplicación", "Se eliminarán todos los datos de la aplicación ¿Desea continuar?", "Si", "No");

            if (answer)
            {
                App.Database.ResetDataBase();
                Json.LoadActivities();
                Application.Current.MainPage = new LoginView();

            }
            
            SelectItem = null;
        }
    }
}
