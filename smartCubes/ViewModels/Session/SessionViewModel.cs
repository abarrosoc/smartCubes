using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Session;
using Syncfusion.XlsIO;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using smartCubes.Enum;

namespace smartCubes.ViewModels.Session
{
    public class SessionViewModel : BaseViewModel
    {
        private UserModel User;
        public INavigation Navigation { get; set; }

        public SessionViewModel(INavigation navigation, UserModel user)
        {
            Navigation = navigation;
            User = user;

            Title = "Sesiones";
            Loading = false;
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

        private bool _Loading;
        public bool Loading
        {
            get
            {
                return _Loading;
            }
            set
            {
                _Loading = value;
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
                Loading = true;
                //await Task.Delay(200);
                await Task.Run(() =>
                {
                List<SessionInit> lSessionInit = App.Database.GetSessionInit(session.ID);

                    if (lSessionInit == null || lSessionInit.Count == 0)
                    {
                        Loading = false;
                        Application.Current.MainPage.DisplayAlert("No hay datos", "La sesión no se puede exportar, aún no contiene datos", "Aceptar");
                    }
                    else
                    {
                        //Set the default application version as Excel 2013.
                        excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2013;
                        //Create a workbook with a worksheet
                        IWorkbook workbook = excelEngine.Excel.Workbooks.Create(lSessionInit.Count);
                        IStyle headerStyle = workbook.Styles.Add("HeaderStyle");
                        headerStyle.BeginUpdate();
                        headerStyle.Color = Syncfusion.Drawing.Color.FromArgb(29, 161, 242);
                        headerStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.White;
                        headerStyle.Font.Bold = true;
                        headerStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                        headerStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        headerStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                        headerStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                        headerStyle.EndUpdate();

                        IStyle bodyStyle = workbook.Styles.Add("BodyStyle");
                        bodyStyle.BeginUpdate();
                        bodyStyle.Color = Syncfusion.Drawing.Color.White;
                        bodyStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                        bodyStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        bodyStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                        bodyStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                        bodyStyle.EndUpdate();

                        //Access first worksheet from the workbook instance.
                        ActivityModel activity = Json.GetActivityByName(session.ActivityName);

                        int cont = 0;
                        foreach (SessionInit sessionInit in lSessionInit)
                        {
                            IWorksheet worksheet = workbook.Worksheets[cont];

                            //Adding text to a cell
                            worksheet[1, 1].Value = "Sesión";
                            worksheet[2, 1].Value = "Actividad";
                            worksheet[3, 1].Value = "Fecha";
                            worksheet[4, 1].Value = "Duración";
                            worksheet[5, 1].Value = "Profesional";
                            worksheet[6, 1].Value = "Alumno";

                            UserModel user = App.Database.GetUser(App.Database.GetSession(sessionInit.SessionId).UserID);
                            worksheet[1, 2].Value = session.Name;
                            worksheet[2, 2].Value = session.ActivityName;
                            worksheet[3, 2].Text = sessionInit.Date.ToString("dd/MM/yyyy HH:mm:ss");
                            worksheet[4, 2].Text = sessionInit.Time;
                            worksheet[5, 2].Value = user.UserName;
                            worksheet[6, 2].Value = sessionInit.StudentCode;

                            worksheet.Name = cont.ToString() + " - " + sessionInit.StudentCode;

                            worksheet[1, 1].CellStyle = headerStyle;
                            worksheet[2, 1].CellStyle = headerStyle;
                            worksheet[3, 1].CellStyle = headerStyle;
                            worksheet[4, 1].CellStyle = headerStyle;
                            worksheet[5, 1].CellStyle = headerStyle;
                            worksheet[6, 1].CellStyle = headerStyle;

                            int column = 1;
                            foreach (MessageDevice message in activity.Messages)
                            {
                                foreach (FieldMessage field in message.Fields)
                                {
                                    worksheet[9, column].Value = field.Description;
                                    worksheet[9, column].CellStyle = headerStyle;
                                    column++;
                                }
                            }

                            Dictionary<MessageDevice, int> messagesColumns = new Dictionary<MessageDevice, int>();
                            int calculateColumn = 1;

                            foreach (MessageDevice message in activity.Messages)
                            {
                                messagesColumns.Add(message, calculateColumn);
                                calculateColumn += message.Fields.Count();
                            }

                            List<SessionData> sessionsData = App.Database.GetSessionData(sessionInit.ID);

                            int row = 10;
                            column = 1;
                            int numByte = 0;

                            for (int j = 0; j < sessionsData.Count; j++)
                            {
                                MessageDevice messageType1 = activity.Messages.Find(m => m.Fields.Sum(f => f.Bytes) == sessionsData[j].Data.Length);

                                if (messageType1 == null)
                                {
                                    int size = Convert.ToInt32(string.Concat(sessionsData[j].Data[3].ToString("X2")), 16) + 4;
                                    SessionData sessionDataComplementary = sessionsData.Find(s => s.Data.Length + sessionsData[j].Data.Length == size && s.DeviceName.Equals(sessionsData[j].DeviceName));

                                    byte[] messageTemp = null;

                                    if (sessionDataComplementary != null)
                                    {
                                        messageTemp = new byte[sessionsData[j].Data.Length + sessionDataComplementary.Data.Length];
                                        Array.Copy(sessionsData[j].Data, 0, messageTemp, 0, sessionsData[j].Data.Length);
                                        Array.Copy(sessionDataComplementary.Data, 0, messageTemp, sessionsData[j].Data.Length, sessionDataComplementary.Data.Length);
                                        messageType1 = activity.Messages.Find(m => m.Fields.Sum(f => f.Bytes) == messageTemp.Length);
                                    }

                                    if (messageType1 != null)
                                    {
                                        column = messagesColumns[messageType1];
                                        numByte = 0;
                                        foreach (FieldMessage field in messageType1.Fields)
                                        {
                                            string m = Convert.ToInt32(string.Concat(messageTemp.Skip(numByte).Take(field.Bytes).ToArray().Select(b => b.ToString("X2"))), 16).ToString();
                                            if (field.Format.Equals("ms"))
                                            {
                                                double millis = Convert.ToDouble(m);
                                                worksheet[row, column].NumberFormat = "###,###,##0.0#########";
                                                worksheet[row, column].Number = millis / 1000.0; ;
                                            }
                                            else
                                            {
                                                worksheet[row, column].Text = m;
                                            }
                                            worksheet[row, column].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
                                            column++;
                                            numByte += field.Bytes;
                                        }
                                        row++;
                                    }
                                }
                                else
                                {
                                    column = messagesColumns[messageType1];
                                    numByte = 0;
                                    foreach (FieldMessage field in messageType1.Fields)
                                    {
                                        string m = Convert.ToInt32(string.Concat(sessionsData[j].Data.Skip(numByte).Take(field.Bytes).ToArray().Select(b => b.ToString("X2"))), 16).ToString();
                                        worksheet[row, column].Value = m;

                                        column++;
                                        numByte += field.Bytes;
                                    }
                                    row++;
                                }
                            }

                            worksheet.UsedRange.AutofitColumns();
                            cont++;
                        }

                        MemoryStream stream = new MemoryStream();
                        workbook.SaveAs(stream);
                        workbook.Close();

                        string filepath = DependencyService.Get<ISave>().SaveAndView("SmartGames_" + session.Name.Replace(" ", "") + ".xlsx", "application/msexcel", stream);

                        Mail mail = new Mail(filepath, User);
                }
                });
                Loading = false;
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
                await Application.Current.MainPage.DisplayAlert("Información", "La sesión se ha eliminado", "Aceptar");
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

            Navigation.PushAsync(new SessionEditView(Navigation, User, false,null));
        }

        private ICommand _OnItemTapped;
        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(() => OnItemTappedExecute())); }
        }

        private void OnItemTappedExecute()
        {
            Loading = true;
            Task.Delay(200);
            Navigation.PushAsync(new SessionFormView(Navigation, User, true, SelectItem));
            SelectItem = null;
            Loading = false;
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
            List<SessionModel> listSessions = null;
            if (User.Role == Role.Admin)
            {
                listSessions = App.Database.GetSessions();
            }
            else
            {
                listSessions = App.Database.GetSessionsByUser(User);
            }

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
