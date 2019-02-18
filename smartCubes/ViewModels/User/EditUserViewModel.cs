using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using smartCubes.Enum;
using smartCubes.Models;
using smartCubes.Utils;
using Xamarin.Forms;

namespace smartCubes.ViewModels.User
{
    public class EditUserViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private bool modify{ get; set; }
        private UserModel user{ get; set; }
        private UserModel userLogin { get; set; }

        public EditUserViewModel(INavigation navigation,UserModel userLogin, bool modify, UserModel user)
        {
            this.Navigation = navigation;
            this.modify = modify;
            this.user = user;
            this.userLogin = userLogin;

            lRoles = new ObservableCollection<String>();

            if(userLogin.Role.Equals(Role.Admin))
                lRoles.Add(Role.Admin);
            lRoles.Add(Role.User);

            if (modify)
            {
                Title = "Modificar";
                UserName = user.UserName;
                Password = Crypt.Decrypt(user.Password, "uah2019");
                Email = user.Email;
                SelectedRole = user.Role;

            }else{
                Title = "Nuevo";
            }
           
        }

        private string _UserName;

        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
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

        private string _Email;

        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                RaisePropertyChanged();
            }
        }
        private String _SelectedRole;

        public String SelectedRole
        {
            get
            {
                return _SelectedRole;
            }
            set
            {
                _SelectedRole = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<String> _lRoles;

        public ObservableCollection<String> lRoles
        {
            get
            {
                return _lRoles;
            }
            set
            {
                _lRoles = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new Command(() => SaveCommandExecuteAsync())); }
        }

        private async void SaveCommandExecuteAsync()
        {
            if(String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(Password)|| String.IsNullOrEmpty(Email) || SelectedRole == null){
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos lo campos", "Aceptar");
            }else{
                bool resultValidate = validateEmail(Email);

                if(resultValidate){
                    UserModel newUser = new UserModel();
                    if (modify)
                        newUser.ID = user.ID;
                    newUser.UserName = UserName;
                    newUser.Password = Crypt.Encrypt(Password, "uah2019");
                    newUser.Email = Email;
                    newUser.Role = SelectedRole;

                    App.Database.SaveUser(newUser);
                    if (modify)
                        await Application.Current.MainPage.DisplayAlert("Información", "El usuario se ha modificado correctamente", "Aceptar");
                    else
                        await Application.Current.MainPage.DisplayAlert("Información", "El usuario se ha creado correctamente", "Aceptar");

                    await Navigation.PopAsync();

                    UserName = "";
                    Password = "";
                    Email = "";
                } 
              
            }

        }

        private bool validateEmail(String email){
            string[] emailSplitAt = email.Split('@');

            if (emailSplitAt.Length != 2)
            {
                Application.Current.MainPage.DisplayAlert("Email incorrecto", "El email introducido no tiene un formato válido", "Aceptar");
                return false;
            }

            string[] emailSplitDot = emailSplitAt[1].Split('.');

            if(emailSplitDot.Length != 2 || (!emailSplitDot[1].Equals("com") && !emailSplitDot[1].Equals("es") && !emailSplitDot[1].Equals("net") && !emailSplitDot[1].Equals("org"))){
                Application.Current.MainPage.DisplayAlert("Email incorrecto", "El email introducido no tiene un formato válido", "Aceptar");
                return false;
            }
            return true;  
        }
    }
}
