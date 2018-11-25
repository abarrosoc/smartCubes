using System;

using Xamarin.Forms;
using smartCubes.Models;
using System.Collections.ObjectModel;
using smartCubes.Utils;
using System.Windows.Input;

namespace smartCubes.ViewModels.Activity
{
    public class ActivityViewModel : BaseViewModel
    {
        public ActivityViewModel()
        {
            Title = "Actividades";
            ActivitiesModel activities = Json.getActivities();
            lActivities = new ObservableCollection<ActivityModel>();

            foreach (ActivityModel activity in activities.Activities)
                lActivities.Add(activity);
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

        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new Command<ActivityModel>((activity) => DeleteCommandExecute(activity))); }
        }

        private void DeleteCommandExecute(ActivityModel activity)
        {
            Json.deleteActivity(activity);
        }
    }
}

