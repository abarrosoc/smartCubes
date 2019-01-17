﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Navigation;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Session;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Menu
{
    public class HomeViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private UserModel user;

        public HomeViewModel(INavigation navigation, UserModel user)
        {
            this.Navigation = navigation;
            this.user = user;
            isVisibleLabel = false;
            isVisibleList = false;
            Title = "Inicio";
            RefreshData();
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

        private ICommand _OnItemTapped;

        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(() => OnItemTappedExecute())); }
        }

        private void OnItemTappedExecute()
        {
            SessionModel item = SelectItem;
            SelectItem = null;
            RefreshData();
            Navigation.PushAsync(new PlaySessionView(item));
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
            List<SessionModel> listSessions = App.Database.GetSessionsByUser(user);

            foreach (SessionModel session in listSessions)
                lSessions.Add(session);

            if (lSessions.Count > 0)
                isVisibleList = true;
            else
                isVisibleLabel = true;
        }
    }
       
}

