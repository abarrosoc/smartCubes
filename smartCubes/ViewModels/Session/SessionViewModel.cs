using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using smartCubes.Models;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Session
{
    public class SessionViewModel : BaseViewModel
    {
        public SessionViewModel()
        {
            Title = "Sesiones";

            lSessions = new ObservableCollection<SessionModel>();
            List<SessionModel> listSessions = App.Database.GetSessions();

            foreach (SessionModel session in listSessions)
                lSessions.Add(session);
        }

        private ObservableCollection<SessionModel> _lSessions;

        public ObservableCollection<SessionModel> lSessions
        {
            get
            {
                return _lSessions;
            }
            set
            {
                _lSessions = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _exportCommand;
        public ICommand ExportCommand
        {
            get { return _exportCommand ?? (_exportCommand = new Command(() => ExportCommandExecute())); }
        }

        private void ExportCommandExecute()
        { 
        }

        private ICommand _DeleteCommand;

        public ICommand DeleteCommand
        {
            get { return _DeleteCommand ?? (_DeleteCommand = new Command<SessionModel>((session) => DeleteCommandExecute(session))); }
        }

        private async void DeleteCommandExecute(SessionModel session)
        {
            
            var answer = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Desea eliminar la sesión?", "Si","No");

            if (answer)
            {
                App.Database.DeleteSession(session);
                await Application.Current.MainPage.DisplayAlert("Info", "La sesión se ha eliminado", "OK");
                RefreshData();
            }
        }

        private void RefreshData()
        {
            lSessions = new ObservableCollection<SessionModel>();
            List<SessionModel> listSessions = App.Database.GetSessions();

            foreach (SessionModel session in listSessions)
                lSessions.Add(session);
        }
    }
}
