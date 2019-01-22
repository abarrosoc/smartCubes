using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Activity;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Activity
{
    public class EditActivityViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private bool modify;
        private ActivityModel activity;

        public EditActivityViewModel(INavigation navigation, bool modify, ActivityModel activity)
        {
            this.Navigation = navigation;
            this.modify = modify;
            this.activity = activity;

            if(modify){
                Title = "Modificar";
                Name = activity.Name;
                Description = activity.Description;
                lDevices = new ObservableCollection<DeviceModel>();
                foreach(DeviceModel device in activity.Devices){
                    lDevices.Add(device);
                }

            }else{
                Title = "Nueva";
            }
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
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos lo campos", "Aceptar");
            }
            else
            {
                ActivityModel newActivity = new ActivityModel();

                if (modify)
                    newActivity.Id = activity.Id;
                newActivity.Name = Name;
                newActivity.Description = Description;
                List<DeviceModel> devices = new List<DeviceModel>();
                foreach (DeviceModel device in lDevices)
                    devices.Add(device);
                
                newActivity.Devices = devices;
                bool isAdd = false;
                if(modify)
                    isAdd = Json.updateActivity(newActivity);
                else
                    isAdd = Json.addActivity(newActivity);

                if(isAdd){
                    if(modify)
                        await Application.Current.MainPage.DisplayAlert("Información", "La actividad ha sido modificada correctamente", "Aceptar");
                    else
                        await Application.Current.MainPage.DisplayAlert("Información", "La actividad ha sido guardada correctamente", "Aceptar");
                    
                    await Navigation.PopAsync();
                    lDevices = new ObservableCollection<DeviceModel>();
                    Name = null;
                    Description = null;
                }else{
                    await Application.Current.MainPage.DisplayAlert("Error", "El nombre de la actividad ya existe", "Aceptar");
                }
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
            if (activity!=null && activity.Devices.Contains(device)) { 
                activity.Devices.Remove(device);
                Json.updateActivity(activity);
            }
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

