using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Session;
using Syncfusion.XlsIO;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Session
{
    public class SessionViewModel : BaseViewModel
    {
        private UserModel user;
        public INavigation Navigation { get; set; }

        public SessionViewModel(INavigation navigation, UserModel user)
        {
            this.Navigation = navigation;
            this.user = user;

            Title = "Sesiones";

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

        private ICommand _exportCommand;
        public ICommand ExportCommand
        {
            get { return _exportCommand ?? (_exportCommand = new Command<SessionModel>((session) => ExportCommandExecute(session))); }
        }

        private async void ExportCommandExecute(SessionModel session)
        { 
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                List<SessionInit> lSessionInit = App.Database.GetSessionInit(session.ID);

                if (lSessions == null)
                {
                    await Application.Current.MainPage.DisplayAlert("No hay datos", "La sesión no se puede exportar, aún no contiene datos", "Aceptar");
                }
                else
                {
                    //Set the default application version as Excel 2013.
                    excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2013;

                    //Create a workbook with a worksheet
                    IWorkbook workbook = excelEngine.Excel.Workbooks.Create(lSessionInit.Count);

                    //Access first worksheet from the workbook instance.

                    int cont = 0;
                    foreach (SessionInit sessionInit in lSessionInit)
                    {
                        IWorksheet worksheet = workbook.Worksheets[cont];

                        //Adding text to a cell
                        worksheet[1, 1].Value = "Sesión";
                        worksheet[1, 2].Value = "Actividad";
                        worksheet[1, 3].Value = "Profesional";
                        worksheet[1, 4].Value = "Alumno";

                        UserModel user = App.Database.GetUser(App.Database.GetSession(sessionInit.SessionId).UserID);
                        worksheet[2, 1].Value = session.Name;
                        worksheet[2, 2].Value = session.ActivityName;
                        worksheet[2, 3].Value = user.UserName;
                        worksheet[2, 4].Value = sessionInit.StudentCode;

                        worksheet.Name = sessionInit.StudentCode;

                        List<SessionData> sessionsData = App.Database.GetSessionData(sessionInit.ID);

                        int i = 4;
                        foreach (SessionData sd in sessionsData)
                        {
                            worksheet[i, 1].Value = sd.Data;
                            i++;
                        }
                        cont++;
                    }

                    //Save the workbook to stream in xlsx format. 
                    MemoryStream stream = new MemoryStream();
                    workbook.SaveAs(stream);

                    workbook.Close();

                    //Save the stream as a file in the device and invoke it for viewing
                    Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView(session.Name + ".xlsx", "application/msexcel", stream);
                    Mail mail = new Mail(session.Name + ".xlsx", user);
                }
            }

        }

        private ICommand _DeleteCommand;

        public ICommand DeleteCommand
        {
            get { return _DeleteCommand ?? (_DeleteCommand = new Command<SessionModel>((session) => DeleteCommandExecute(session))); }
        }

        private async void DeleteCommandExecute(SessionModel session)
        {
            
            var answer = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Desea eliminar la sesión?", "Si","No");

            if (answer)
            {
                App.Database.DeleteSession(session);
                await Application.Current.MainPage.DisplayAlert("Info", "La sesión se ha eliminado", "Aceptar");
                RefreshData();
            }
        }

        private ICommand _NewSessionCommand;

        public ICommand NewSessionCommand
        {
            get { return _NewSessionCommand ?? (_NewSessionCommand = new Command(() => NewSessionCommandExecute())); }
        }

        private void NewSessionCommandExecute()
        {
            Navigation.PushAsync(new NewSessionView(Navigation, user,false,null));
        }

        private ICommand _OnItemTapped;

        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(() => OnItemTappedExecute())); }
        }

        private void OnItemTappedExecute()
        {
            Navigation.PushAsync(new NewSessionView(Navigation, user, true, SelectItem));
            SelectItem = null;
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

        public void  RefreshData()
        {
            lSessions = new ObservableCollection<SessionModel>();
            List<SessionModel> listSessions = App.Database.GetSessions();

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
