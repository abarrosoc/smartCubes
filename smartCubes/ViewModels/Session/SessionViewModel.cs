using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Session;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Session
{
    public class SessionViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public SessionViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

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

        private SessionModel _SelectItem;

        public SessionModel SelectItem
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

        private bool _isRefreshing = false;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(nameof(IsRefreshing));
            }
        }

        private ICommand _exportCommand;
        public ICommand ExportCommand
        {
            get { return _exportCommand ?? (_exportCommand = new Command(() => ExportCommandExecute())); }
        }

        private void ExportCommandExecute()
        { 
            Mail mail = new Mail();
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

        private ICommand _NewSessionCommand;

        public ICommand NewSessionCommand
        {
            get { return _NewSessionCommand ?? (_NewSessionCommand = new Command(() => NewSessionCommandExecute())); }
        }

        private void NewSessionCommandExecute()
        {
            Navigation.PushAsync(new NewSessionView());
        }

        private ICommand _OnItemTapped;

        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command<SessionViewModel>((session) => OnItemTappedExecute(session))); }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    RefreshData();

                    IsRefreshing = false;
                });
            }
        }

        private void OnItemTappedExecute(SessionViewModel session)
        {
            Debug.WriteLine("Behaviorsss!");
        }

        private void  RefreshData()
        {
            lSessions = new ObservableCollection<SessionModel>();
            List<SessionModel> listSessions = App.Database.GetSessions();

            foreach (SessionModel session in listSessions)
                lSessions.Add(session);
        }
    }
}
