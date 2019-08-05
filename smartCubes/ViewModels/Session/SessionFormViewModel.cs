using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.Utils;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Session
{
    public class EditSessionViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        private UserModel User;
        private bool Modify;
        private SessionModel Session;

        public EditSessionViewModel(INavigation navigation, UserModel user,bool modify,SessionModel session)
        {
            Navigation = navigation;
            User = user;
            Modify = modify;
            Session = session;
                
            ActivitiesModel activities = Json.GetActivities();
            lActivities = new ObservableCollection<ActivityModel>();
            foreach (ActivityModel activity in activities.Activities)
                lActivities.Add(activity);
            
            isEnabledPicker = true;

            if(modify)
            {
                Name = session.Name;
                Description = session.Description;
                ActivityModel activitySession = Json.GetActivityByName(session.ActivityName);
                foreach (ActivityModel activity in activities.Activities){
                    if(activity.Name.Equals(activitySession.Name)){
                        SelectedActivity = activity;
                    }
                }
                lSessionsInit = new ObservableCollection<SessionInit>();
                RefreshData();
                Title = "Editar sesión";
            }
            else{
                Title = "Nueva sesión";
                isVisible = false;
                Name = GetNameNewSession();
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

        private string _Description;
        public string Description
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

        private ActivityModel _SelectedActivity;
        public ActivityModel SelectedActivity
        {
            get
            {
                return _SelectedActivity;
            }
            set
            {
                _SelectedActivity = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<ActivityModel> _lActivities;
        public ObservableCollection<ActivityModel> lActivities
        {
            get
            {
                return _lActivities;
            }
            set
            {
                _lActivities = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<SessionInit> _lSessionsInit;

        public ObservableCollection<SessionInit> lSessionsInit
        {
            get
            {
                return _lSessionsInit;
            }
            set
            {
                _lSessionsInit = value;
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

        private bool _isVisible = false;
        public bool isVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        } 

        private bool _isEnabledPicker = false;
        public bool isEnabledPicker
        {
            get { return _isEnabledPicker; }
            set
            {
                _isEnabledPicker = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new Command(() => SaveCommandExecuteAsync())); }
        }

        private async void SaveCommandExecuteAsync()
        {
            if (SelectedActivity != null)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos lo campos", "Aceptar");
            }
            else
            {
                SessionModel newSession = new SessionModel();
                if (Modify)
                    newSession.ID = Session.ID;
                newSession.Name = Name;
                newSession.Description = Description;
                newSession.ActivityName = SelectedActivity.Name;
                if (!Modify)
                    newSession.CreateDate = DateTime.Now;
                else
                    newSession.CreateDate = Session.CreateDate;
                newSession.ModifyDate = DateTime.Now;
                newSession.UserID = User.ID;

                App.Database.SaveSession(newSession);

                if (Modify)
                {
                    await Application.Current.MainPage.DisplayAlert("Información", "La sesión se ha modificado correctamente", "Aceptar");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Información", "La sesión se ha creado correctamente", "Aceptar");
                }

                await Navigation.PopAsync();

                Name = null;
                Description = "";
                SelectedActivity = null;

            }
        }
        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get { return _DeleteCommand ?? (_DeleteCommand = new Command<SessionInit>((session) => DeleteCommandExecute(session))); }
        }

        private async void DeleteCommandExecute(SessionInit session)
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Desea eliminar el elemento?", "Si", "No");

            if (answer)
            {
                App.Database.DeleteSessionInit(session);
                await Application.Current.MainPage.DisplayAlert("Información", "Eliminado correctamente", "Aceptar");
                RefreshData();
            }
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

        private void RefreshData()
        {
            lSessionsInit = new ObservableCollection<SessionInit>();
            List<SessionInit> sessionsInit = App.Database.GetSessionInit(Session.ID);

            if (sessionsInit.Count > 0){
                isEnabledPicker = false;
                isVisible = true;
            } else {
                isVisible = false;
            }
            foreach (SessionInit session in sessionsInit)
                lSessionsInit.Add(session);

        }

        private string GetNameNewSession(){
            List<SessionModel> listSessions = App.Database.GetSessions();
            return "Sesión " + (listSessions.Count+1).ToString();
        }
    }
}
