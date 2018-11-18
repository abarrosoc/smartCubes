using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using smartCubes.Models;

namespace smartCubes.ViewModels.Session
{
    public class SessionViewModel : BaseViewModel
    {
        public SessionViewModel()
        {
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
    }
}
