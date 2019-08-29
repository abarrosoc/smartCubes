using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Menu;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public LoginViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Loading = false;
        } 

        private string _User;

        public string User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                RaisePropertyChanged();
            }
        }

        private string _Password;

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
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

        public static MasterDetailPage MasterDetail { get; set; }
        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get { return _LoginCommand ?? (_LoginCommand = new Command(() => LoginCommandExecuteAsync())); }
        }

        private async void LoginCommandExecuteAsync()
        {
            bool access = false;
            try
            {

                var statusLocation = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (statusLocation != PermissionStatus.Granted)
                {

                    var requestedPermissionsLocation = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    statusLocation = requestedPermissionsLocation[Permission.Location];
                }

                if (statusLocation == PermissionStatus.Granted)
                {
                    var statusStorage = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                    if (statusStorage != PermissionStatus.Granted)
                    {

                        var requestedPermissionsStorage = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                        statusStorage = requestedPermissionsStorage[Permission.Storage];
                    }

                    if (statusLocation == PermissionStatus.Granted && statusStorage == PermissionStatus.Granted)
                    {
                        access = true;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Acceso denegado", "Es necesario aceptar los permisos para poder continuar", "Aceptar");
                        return;

                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Acceso denegado", "Es necesario aceptar los permisos para poder continuar", "Aceptar");
                    return;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Se ha producido un error inesperado y no es posible continuar.", "Aceptar");
            }

            if (access)
            {
                bool login = false;
                Loading = true;
                await Task.Run(() =>
                {

                    if (App.Database.GetUsers().Count == 0)
                    {
                        App.Database.ResetDataBase();
                        Json.LoadActivities();
                    }
                  //  UserModel user = App.Database.GetUsers()[0];
                  //  Application.Current.MainPage = new MainPage(user);
                    
                    //Application.Current.MainPage = new MainPage();
                    if(User!=null || Password!=null){
                        UserModel UserDb = App.Database.GetUser(User);
                        if (UserDb != null)
                        {
                            string userPass = Crypt.Decrypt(UserDb.Password, "uah2019");
                            if (Password.Equals(userPass))
                            {
                                Application.Current.MainPage = new MainPage(UserDb);
                                login = true;
                            } 
                        }
                    }
                });

                if(login== false)
                {
                    await Application.Current.MainPage.DisplayAlert("Login", "Usuario o contraseña incorrecto", "Aceptar");
                    User = "";
                    Password = "";
                }

                Loading = false;
            }
        }
    }
}
