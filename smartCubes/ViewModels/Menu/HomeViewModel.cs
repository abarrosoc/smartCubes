using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.View.Session;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Menu
{
    public class HomeViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public HomeViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            Title = "Inicio";
                
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

        private ICommand _OnItemTapped;

        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(() => OnItemTappedExecute())); }
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

        private void OnItemTappedExecute()
        {
            Navigation.PushAsync(new PlaySessionView(SelectItem));
            SelectItem = null;
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

