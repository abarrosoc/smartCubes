using System;
using System.Collections.Generic;
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
            Title = "Configuración";

            SettingModel settingModel = new SettingModel();
            settingModel.Text = "Restaurar aplicación";
            settingModel.Color = "Red";

            lSettings.Add(settingModel);

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

        private String _SelectItem;

        public String SelectItem
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

        private ICommand _OnItemTapped;

        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(() => OnItemTappedExecute())); }
        }

        private async void OnItemTappedExecute()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Restaurar aplicación", "Se eliminarán todos los datos de la aplicación ¿Desea continuar?", "Si", "No");

            if (answer)
            {
                App.Database.ResetDataBase();
                Json.loadActivities();
                App.Current.MainPage = new LoginView();

            }
            
            SelectItem = null;
        }
    }
}
