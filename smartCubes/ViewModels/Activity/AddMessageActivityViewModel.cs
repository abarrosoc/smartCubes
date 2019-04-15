using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.View.Activity;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Activity
{
    public class AddMessageActivityViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private bool modify;
        private ActivityModel activity;

        public AddMessageActivityViewModel(INavigation navigation, ActivityModel activity, bool modify)
        {
            this.Navigation = navigation;
            this.modify = modify;
            this.activity = activity;

            lMessages = new ObservableCollection<MessageDevice>();

            if(activity!=null && activity.Messages != null){
                foreach ( MessageDevice message in activity.Messages){
                    lMessages.Add(message);
                }
            }
        }

        private ObservableCollection<MessageDevice> _lMessages;

        public ObservableCollection<MessageDevice> lMessages
        {
            get
            {
                return _lMessages;
            }
            set
            {
                _lMessages = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _onItemTapped;

        public ICommand OnItemTapped
        {
            get { return _onItemTapped ?? (_onItemTapped = new Command<DeviceModel>((device) => OnItemTappedExecute(device))); }
        }
        private void OnItemTappedExecute(DeviceModel device)
        {
            Navigation.PushAsync(new AddDevicePopUp(this, modify));
        }
     
        private ICommand _addDeviceCommand;

        public ICommand AddDeviceCommand
        {
            get { return _addDeviceCommand ?? (_addDeviceCommand = new Command(() => AddDeviceCommandExecute())); }
        }
        private void AddDeviceCommandExecute()
        {
            MessageDevice messageDevice = new MessageDevice();
           
            DeviceModel newDevice = new DeviceModel();
            newDevice.Name = NameDevice;
            newDevice.Uuid = Uuid;
            newDevice.State = DeviceState.Disconnected.ToString();
            lDevices.Add(newDevice);
            NameDevice = null;
            Uuid = null;

        }
    }
}
