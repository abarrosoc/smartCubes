using System;

using Xamarin.Forms;
using smartCubes.Models;
using System.Collections.ObjectModel;
using smartCubes.Utils;

namespace smartCubes.ViewModels.Activity
{
    public class ActivityViewModel : BaseViewModel
    {
        public ActivityViewModel()
        {
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
    }
}

