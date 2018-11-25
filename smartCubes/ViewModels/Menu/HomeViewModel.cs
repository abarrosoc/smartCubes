using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.View;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Menu
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            lSessions = new ObservableCollection<SessionModel>();

           // List<SessionModel> listSessions = App.Database.GetSessions();

           // foreach (SessionModel session in listSessions)
           //     lSessions.Add(session);
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
    }
}

