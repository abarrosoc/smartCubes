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
    public class AddDevicePopUpViewModel : BaseViewModel
    {
        public AddMessageActivityViewModel addMessageActivityViewModel { get; set; }

        private bool isModified { get; set; }

        public AddDevicePopUpViewModel(AddMessageActivityViewModel addMessageActivityViewModel, bool isModified)
        {
            this.isModified = isModified;
            lSizes = new ObservableCollection<int>();
            for (int i = 1; i < 33; i++)
            {
                lSizes.Add(i);
            }

            this.addMessageActivityViewModel = addMessageActivityViewModel;
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

        private int _SizePicker;

        public int SizePicker
        {
            get
            {
                return _SizePicker;
            }
            set
            {
                _SizePicker = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<FieldMessage> _lFields;

        public ObservableCollection<FieldMessage> lFields
        {
            get
            {
                return _lFields;
            }
            set
            {
                _lFields = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<int> _lSizes;

        public ObservableCollection<int> lSizes
        {
            get
            {
                return _lSizes;
            }
            set
            {
                _lSizes = value;
                RaisePropertyChanged();
            }
        }
        private FieldMessage _SelectField;

        public FieldMessage SelectField
        {
            get
            {
                return _SelectField;
            }
            set
            {
                _SelectField = value;
                RaisePropertyChanged();
            }
        }
        private int _SelectSize;

        public int SelectSize
        {
            get
            {
                return _SelectSize;
            }
            set
            {
                _SelectSize = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _AddCommand;

        public ICommand AddCommand
        {
            get { return _AddCommand ?? (_AddCommand = new Command(() => OnButtonAddDeviceClickedExecute())); }
        }
        private async void OnButtonAddDeviceClickedExecute()
        {
          /*  if (String.IsNullOrEmpty(Name) || String.IsNullOrWhiteSpace(Name) || String.IsNullOrEmpty(Uuid) || String.IsNullOrWhiteSpace(Uuid) || lFields.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos lo campos", "Aceptar");
                return;
            }

            DeviceModel device = new DeviceModel();
            device.Name = Name;
            device.State = "Disconnected";
            device.Uuid = Uuid;


            if (newActivityView.lDevices == null)
            {
                newActivityView.lDevices = new ObservableCollection<DeviceModel>();
            }
            else if (newActivityView.lDevices.Contains(device))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "No puede añadir dos dispositivos con el mismo uuid y/o nombre", "Aceptar");
                return;
            }
            if (isModified)
            {
                for (int i = 0; i < newActivityView.lDevices.Count; i++)
                {
                    if (newActivityView.lDevices[i].Uuid.Equals(newActivityView.SelectDevice.Uuid))
                    {
                        newActivityView.lDevices[i].Name = device.Name;
                        newActivityView.lDevices[i].Uuid = device.Uuid;
                    }
                }
                newActivityView.RefreshData();
            }
            else
            {
                newActivityView.lDevices.Add(device);

               
            }
            newActivityView.SelectDevice = null;


            await PopupNavigation.Instance.PopAsync();*/
        }

        private ICommand _addFieldCommand;

        public ICommand AddFieldCommand
        {
            get { return _addFieldCommand ?? (_addFieldCommand = new Command(() => AddFieldCommandExecute())); }
        }

        private async void AddFieldCommandExecute()
        {
            if (String.IsNullOrEmpty(Description) || SelectSize.Equals(0))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar los campos obligatorios", "Aceptar");
                return;
            } 
            foreach(FieldMessage f in lFields){
                if(f.Description.Equals(Description))
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Ya existe un campo con la misma descripción", "Aceptar");
                    return;
                }
            }
            FieldMessage field = new FieldMessage();
            field.Bytes = SelectSize;
            field.Description = Description;
            lFields.Add(field);

            SelectSize = 0;
            Description = null;
        }
        private ICommand _cancelFieldCommand;

        public ICommand CancelCommand
        {
            get { return _cancelFieldCommand ?? (_cancelFieldCommand = new Command(() => CancelCommandExecute())); }
        }

        private async void CancelCommandExecute()
        {
            await PopupNavigation.Instance.PopAsync();
        }
        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new Command<FieldMessage>((field) => DeleteCommandExecute(field))); }
        }

        private async void DeleteCommandExecute(FieldMessage field)
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Desea eliminar el campo?", "Si", "No");

            if (answer)
            {
                lFields.Remove(field);
            }
        }
        private ICommand _OnItemTapped;

        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(() => OnItemTappedExecute())); }
        }

        private async void OnItemTappedExecute()
        {

            var answer = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Desea eliminar el campo", "Si","No");
            if(answer)
            {
                lFields.Remove(SelectField);
            }
        }

    }
}
