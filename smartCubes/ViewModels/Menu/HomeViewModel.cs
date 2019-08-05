using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using smartCubes.Models;
using smartCubes.View.Session;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Menu
{
    public class HomeViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private UserModel User;

        public HomeViewModel(INavigation navigation, UserModel user)
        {
            Navigation = navigation;
            User = user;
            isVisibleLabel = false;
            isVisibleList = false;
            Title = "Inicio";
            RefreshData();
            Loading = false;
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

        private bool _isVisibleList = false;
        public bool isVisibleList
        {
            get { return _isVisibleList; }
            set
            {
                _isVisibleList = value;
                RaisePropertyChanged();
            }
        } 

        private bool _isVisibleLabel = false;
        public bool isVisibleLabel
        {
            get { return _isVisibleLabel; }
            set
            {
                _isVisibleLabel = value;
                RaisePropertyChanged();
            }
        } 

        private bool _Loading = true;
        public bool Loading
        {
            get { return _Loading; }
            set
            {
                _Loading = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _OnItemTapped;
        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(()  =>  OnItemTappedExecute())); }
        }

        private async void OnItemTappedExecute()
        {
        
            if (!CrossBluetoothLE.Current.State.Equals(BluetoothState.On))
            {
                SelectItem = null;
                await Application.Current.MainPage.DisplayAlert("Atención", "Encienda el bluetooth del dispositivo", "Aceptar");
            }
            else
            {
                Loading = true;
                await Task.Delay(200);
                SessionModel item = SelectItem;
                SelectItem = null;
                await Navigation.PushModalAsync(new PlaySessionView(item));
                Loading = false;
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

        public void RefreshData()
        {
            lSessions = new ObservableCollection<SessionModel>();
            List<SessionModel> listSessions = App.Database.GetSessionsByUser(User);

            foreach (SessionModel session in listSessions)
                lSessions.Add(session);

            if (lSessions.Count > 0)
            {
                isVisibleList = true;
                isVisibleLabel = false;
            }
            else
            {
                isVisibleLabel = true;
                isVisibleList = false;
            }
        }
    } 
}

