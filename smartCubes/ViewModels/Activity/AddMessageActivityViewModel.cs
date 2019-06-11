using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Activity;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Activity
{
    public class AddMessageActivityViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public List<FieldMessage> FieldsTemp { get; set; }

        private bool Modify;
        private ActivityModel Activity;
        private MessageDevice message;

        public AddMessageActivityViewModel(INavigation navigation, ActivityModel activity, bool modify)
        {
            Navigation = navigation;
            Modify = modify;
            Activity = activity;
            FieldsTemp = new List<FieldMessage>();
            lMessages = new ObservableCollection<MessageDevice>();

            if (modify)
            {
                Title = "Editar";
            }
            else
            {
                Title = "Nueva";
            }

            if (activity != null && activity.Messages != null){
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

        private string _Name;

        public string Name
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

        private string _Size;
        public string Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
                RaisePropertyChanged();
            }
        }

        private MessageDevice _SelectMessage;
        public MessageDevice SelectMessage
        {
            get
            {
                return _SelectMessage;
            }
            set
            {
                _SelectMessage = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _addMessageCommand;

        public ICommand AddMessageCommand
        {
            get { return _addMessageCommand ?? (_addMessageCommand = new Command(() => AddMessageCommandExecute())); }
        }
        private void AddMessageCommandExecute()
        {
            List<MessageDevice> lMessagesTemp = new List<MessageDevice>(lMessages);

            if (Size != null && Size.Equals("0"))
            {
                Application.Current.MainPage.DisplayAlert("Atención", "El tamaño debe ser mayor que 0", "Aceptar");
            }
            else if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Size))
            {
                Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos los campos", "Aceptar");
            }
            else if (!Modify && lMessagesTemp.Find( m => m.Name.Equals(Name)) !=  null)
            {
                Application.Current.MainPage.DisplayAlert("Atención", "Ya existe un mensaje con el mismo nombre", "Aceptar");
            }
            else
            {
                if (message == null)
                {
                    FieldsTemp.RemoveAll(f => f.Bytes == 0);
                    message = new MessageDevice();
                    message.Name = Name;
                    message.Fields = FieldsTemp;
                    FieldsTemp = new List<FieldMessage>();
                    lMessages.Add(message);
                }
                else
                {
                    foreach(MessageDevice m in lMessages)
                    {
                        if (m.Name.Equals(message.Name))
                        {
                            message.Fields.RemoveAll(f => f.Bytes == 0);
                            m.Name = message.Name;
                            m.Fields = message.Fields;
                        }
                    }
                }
                Name = "";
                Size = "";
                message = null;
            }
        }

        private ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get { return _SaveCommand ?? (_SaveCommand = new Command(() => SaveCommandExecute())); }
        }

        private async void SaveCommandExecute()
        {
             if (lMessages.Count == 0)
             {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe añadir al menos un mensaje", "Aceptar");
            }
            else
            {
                if (Modify)
                {
                    Activity.Messages.RemoveAll(m => m != null);
                    foreach (MessageDevice message in lMessages)
                    {
                        Activity.Messages.Add(message);
                    }
                    Json.updateActivity(Activity);
                    await Application.Current.MainPage.DisplayAlert("Información", "La actividad se ha modificado correctamente", "Aceptar");
                }
                else
                {
                    Activity.Messages = new List<MessageDevice>();
                    foreach (MessageDevice message in lMessages)
                    {
                        Activity.Messages.Add(message);
                    }
                    Json.addActivity(Activity);
                    await Application.Current.MainPage.DisplayAlert("Información", "La actividad se ha creado correctamente", "Aceptar");
                }
                
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count-1]);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count-1]);
                await Navigation.PopAsync();

            }
        }

        private ICommand _DeleteCommand;

        public ICommand DeleteCommand
        {
            get { return _DeleteCommand ?? (_DeleteCommand = new Command<MessageDevice>((message) => DeleteCommandExecute(message))); }
        }

        private void DeleteCommandExecute(MessageDevice message)
        {
            lMessages.Remove(message);
        }
        private ICommand _OnItemTappedCommand;

        public ICommand OnItemTappedCommand
        {
            get { return _OnItemTappedCommand ?? (_OnItemTappedCommand = new Command(() => OnItemTappedCommandExecute())); }
        }

        private void OnItemTappedCommandExecute()
        {
           //editar?
        }
        private ICommand _addFieldsCommand;

        public ICommand AddFieldsCommand
        {
            get { return _addFieldsCommand ?? (_addFieldsCommand = new Command(() => AddFieldsCommandExecute())); }
        }
        private void AddFieldsCommandExecute()
        {
            PopupNavigation.PushAsync(new AddFieldsPopUp(this, Modify));

        }

       
    }
}
