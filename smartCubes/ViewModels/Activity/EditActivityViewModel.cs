using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Plugin.BLE.Abstractions;
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

            if (modify)
            {
                Title = "Modificar";
                Name = activity.Name;
                Description = activity.Description;


            }
            else
            {
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





        /*  private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new Command(() => SaveCommandExecute())); }
        }

       private async void SaveCommandExecute()
        {
            if (String.IsNullOrEmpty(Name) || lDevices.Count == 0)
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
       */
        private ICommand _nextCommand;

        public ICommand NextCommand
        {
            get { return _nextCommand ?? (_nextCommand = new Command(() => NextCommandExecute())); }
        }

        private async void NextCommandExecute()
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Description))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos lo campos", "Aceptar");
            }
            else
            {
                if (modify)
                {
                    activity.Name = Name;
                    activity.Description = Description;
                    await Navigation.PushAsync(new AddDeviceActivityView(activity, modify));
                }
                else
                {

                    ActivityModel newActivity = new ActivityModel();
                    newActivity.Name = Name;
                    newActivity.Description = Description;
                    await Navigation.PushAsync(new AddDeviceActivityView(newActivity, modify));
                }

               
            }
        }





    }
}

