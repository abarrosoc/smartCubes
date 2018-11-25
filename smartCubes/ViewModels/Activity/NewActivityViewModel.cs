using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using smartCubes.Models;
using smartCubes.Utils;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Activity
{
    public class NewActivityViewModel : BaseViewModel
    {
       // private ObservableCollection<DeviceModel> deviceList;

        public NewActivityViewModel()
        {
            Title = "Nueva actividad";
        }

        private String _Name;

        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                RaisePropertyChanged();
            }
        }

        private String _Description;

        public String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                RaisePropertyChanged();
            }
        }
       
        private string _ColorName;

        public string ColorName
        {
            get
            {
                return _ColorName;
            }
            set
            {
                _ColorName = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new Command(() => SaveCommandExecute())); }
        }

        private async void SaveCommandExecute()
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Description))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos lo campos", "OK");
            }
            else
            {
                ActivityModel activity = new ActivityModel();

                Json.addActivity(activity);
            }
        }

    }
}

