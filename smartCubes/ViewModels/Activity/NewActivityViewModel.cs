using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Activity;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Activity
{
    public class NewActivityViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public NewActivityViewModel(INavigation navigation)
        {
            Title = "Nueva";
            this.Navigation = navigation;
            //lDevices = new ObservableCollection<DeviceModel>();
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

        private ObservableCollection<DeviceModel> _lDevices;

        public ObservableCollection<DeviceModel> lDevices
        {
            get
            {
                return _lDevices;
            }
            set
            {
                _lDevices = value;
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
        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new Command<DeviceModel>((device) => DeleteCommandExecute(device))); }
        }

        private void DeleteCommandExecute(DeviceModel device)
        {
            lDevices.Remove(device);
        }

        private ICommand _OnButtonAddDeviceClicked;

        public ICommand OnButtonAddDeviceClicked
        {
            get { return _OnButtonAddDeviceClicked ?? (_OnButtonAddDeviceClicked = new Command(() => OnButtonAddDeviceClickedExecute())); }
        }
        private void OnButtonAddDeviceClickedExecute()
        {
            PopupNavigation.Instance.PushAsync(new AddDevicePopUp(this));
        }
    }
}

