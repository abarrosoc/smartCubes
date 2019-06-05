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
        public List<FieldMessage> lFieldsTemp { get; set; }

        private bool modify;
        private ActivityModel activity;
        private MessageDevice message;

        public AddMessageActivityViewModel(INavigation navigation, ActivityModel activity, bool modify)
        {
            this.Navigation = navigation;
            this.modify = modify;
            this.activity = activity;
            lFieldsTemp = new List<FieldMessage>();
            lMessages = new ObservableCollection<MessageDevice>();
            if (modify)
            {
                Title = "Modificar";
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

        private String _Size;

        public String Size
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
            if (Size != null && Size.Equals("0"))
            {
                Application.Current.MainPage.DisplayAlert("Atención", "El tamaño debe ser mayor que 0", "Aceptar");
            }
            else if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Size))
            {
                Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos los campos", "Aceptar");
            }
            else
            {
                if (message == null)
                {
                    lFieldsTemp.RemoveAll(f => f.Bytes == 0);
                    message = new MessageDevice();
                    message.Name = Name;
                    message.Fields = lFieldsTemp;
                    lFieldsTemp = new List<FieldMessage>();
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
                Application.Current.MainPage.DisplayAlert("Atención", "Debe añadir al menos un mensaje", "Aceptar");
            }
            else
            {
                if (modify)
                {
                    activity.Messages.RemoveAll(m => m != null);
                    foreach (MessageDevice message in lMessages)
                    {
                        activity.Messages.Add(message);
                    }
                    Json.updateActivity(activity);
                    await Application.Current.MainPage.DisplayAlert("Información", "La actividad se ha modificado correctamente", "Aceptar");
                }
                else
                {
                    activity.Messages = new List<MessageDevice>();
                    foreach (MessageDevice message in lMessages)
                    {
                        activity.Messages.Add(message);
                    }
                    Json.addActivity(activity);
                    await Application.Current.MainPage.DisplayAlert("Información", "La actividad se ha creado correctamente", "Aceptar");
                }
                   
                await Navigation.PopToRootAsync();
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
            PopupNavigation.PushAsync(new AddFieldsPopUp(this, modify));

        }

       
    }
}
