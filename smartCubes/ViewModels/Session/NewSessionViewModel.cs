﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.Utils;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Session
{
    public class NewSessionViewModel : BaseViewModel
    {
        public NewSessionViewModel()
        {
            ActivitiesModel activities = Json.getActivities();
            lActivities = new ObservableCollection<ActivityModel>();

            foreach (ActivityModel activity in activities.Activities)
                lActivities.Add(activity);
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
            get { return _saveCommand ?? (_saveCommand = new Command(() => SaveCommandExecute())); }
        }

        private void SaveCommandExecute()
        {
            SessionModel session = new SessionModel();
            session.Name = Name;
            session.Description = Description;
            session.ActivityName = SelectedActivity.Name;
            session.CreateDate = DateTime.Now;
            session.ModifyDate = DateTime.Now;

            App.Database.SaveSession(session);


        }
    }
}