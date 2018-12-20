﻿using System;
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
        private UserModel user;
        private bool modify;
        private SessionModel session;

        public EditSessionViewModel(INavigation navigation, UserModel user,bool modify,SessionModel session)
        {
            this.Navigation = navigation;
            this.user = user;
            this.modify = modify;
            this.session = session;

            ActivitiesModel activities = Json.getActivities();
            lActivities = new ObservableCollection<ActivityModel>();
            foreach (ActivityModel activity in activities.Activities)
                lActivities.Add(activity);
            
            if(modify){
                Title = "Modificar";
                Name = session.Name;
                Description = session.Description;
                ActivityModel activitySession = Json.getActivityByName(session.ActivityName);
                foreach (ActivityModel activity in activities.Activities){
                    if(activity.Name.Equals(activitySession.Name)){
                        SelectedActivity = activity;
                    }
                }
            }else{
                Title = "Nueva";
                Name = getNameNewSession();
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

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new Command(() => SaveCommandExecuteAsync())); }
        }

        private async void SaveCommandExecuteAsync()
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Description) || String.IsNullOrEmpty(SelectedActivity.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos lo campos", "Aceptar");
            }
            else
            {
                SessionModel newSession = new SessionModel();
                if (modify)
                    newSession.ID = session.ID;
                newSession.Name = Name;
                newSession.Description = Description;
                newSession.ActivityName = SelectedActivity.Name;
                if (!modify)
                    newSession.CreateDate = DateTime.Now;
                else
                    newSession.CreateDate = session.CreateDate;
                newSession.ModifyDate = DateTime.Now;
                newSession.UserID = user.ID;

                App.Database.SaveSession(newSession);
  
                if (modify)
                    await Application.Current.MainPage.DisplayAlert("Información", "La sesión se ha modificado correctamente", "Aceptar");
                else
                    await Application.Current.MainPage.DisplayAlert("Información", "La sesión se ha creado correctamente", "Aceptar");

                await Navigation.PopAsync();

                Name = null;
                Description = "";
                SelectedActivity = null;

            }
        }

        private String getNameNewSession(){

            List<SessionModel> listSessions = App.Database.GetSessions();
            return "Sesión " + (listSessions.Count+1).ToString();
        }
    }
}
