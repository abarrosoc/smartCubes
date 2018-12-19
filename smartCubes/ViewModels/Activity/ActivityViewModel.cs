using System;
using Xamarin.Forms;
using smartCubes.Models;
using System.Collections.ObjectModel;
using smartCubes.Utils;
using System.Windows.Input;
using System.Diagnostics;
using smartCubes.View.Activity;

namespace smartCubes.ViewModels.Activity
{
    public class ActivityViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public ActivityViewModel(INavigation navigation)
        {
            Title = "Actividades";
            this.Navigation = navigation;

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

        private ActivityModel _SelectItem;

        public ActivityModel SelectItem
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

        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new Command<ActivityModel>((activity) => DeleteCommandExecute(activity))); }
        }

        private async void DeleteCommandExecute(ActivityModel activity)
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Desea eliminar la actividad?", "Si", "No");

            if (answer)
            {
                bool isDeleted = Json.deleteActivity(activity);
                if(isDeleted){
                    await Application.Current.MainPage.DisplayAlert("Información", "La actividad se ha eliminado", "Aceptar");
                    RefreshData();
                }else{
                    await Application.Current.MainPage.DisplayAlert("Error", "No se ha podido eliminar la actividad", "Aceptar");
                }

            }
        }

        private ICommand _NewActivityCommand;

        public ICommand NewActivityCommand
        {
            get { return _NewActivityCommand ?? (_NewActivityCommand = new Command(() => NewActivityCommandExecute())); }
        }

        private void NewActivityCommandExecute()
        {
            Navigation.PushAsync(new NewActivityView(false,null));
        }

        private ICommand _OnItemTapped;

        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(() => OnItemTappedExecute())); }
        }

        private void OnItemTappedExecute()
        {
            Navigation.PushAsync(new NewActivityView(true, SelectItem));
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
            ActivitiesModel activities = Json.getActivities();
            lActivities = new ObservableCollection<ActivityModel>();

            foreach (ActivityModel activity in activities.Activities)
                lActivities.Add(activity);
        }
    }
}

