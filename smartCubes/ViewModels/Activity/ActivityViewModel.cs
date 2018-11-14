using System;

using Xamarin.Forms;
using smartCubes.Models;
using System.Collections.ObjectModel;

namespace smartCubes.ViewModels.Activity
{
    public class ActivityViewModel : BaseViewModel
    {
        public ActivityViewModel()
        {
            lActivities = new ObservableCollection<ActivityModel>();
            lActivities.Add(new ActivityModel(1L,"Sesión 1",null));
            lActivities.Add(new ActivityModel(1L, "Sesión 2", null));
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

