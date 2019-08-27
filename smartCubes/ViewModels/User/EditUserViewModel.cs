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

        private bool Modify{ get; set; }
        private UserModel User{ get; set; }

        public EditUserViewModel(INavigation navigation,UserModel userLogin, bool modify, UserModel user)
        {
            Navigation = navigation;
            Modify = modify;
            User = user;
            Eye = "noeye.png";
            lRoles = new ObservableCollection<String>();

            if (userLogin.Role.Equals(Role.Admin))
            {
                lRoles.Add(Role.Admin);
            }
            lRoles.Add(Role.User);
            ViewPassword = true;

            if (modify)
            {
                Title = "Editar usuario";
                UserName = user.UserName;
                Password = Crypt.Decrypt(user.Password, "uah2019");
                Email = user.Email;
                SelectedRole = user.Role;

            }else{
                Title = "Nuevo usuario";
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

        private bool _ViewPassword;
        public bool ViewPassword
        {
            get
            {
                return _ViewPassword;
            }
            set
            {
                _ViewPassword = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectedRole;
        public string SelectedRole
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

        private string _Eye;
        public string Eye
        {
            get
            {
                return _Eye;
            }
            set
            {
                _Eye = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<string> _lRoles;
        public ObservableCollection<string> lRoles
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

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || SelectedRole == null)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar los campos obligatorios", "Aceptar");
            }
            else if (!Modify && App.Database.GetUsers().Find(u => u.UserName.Equals(UserName)) != null)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Ya existe un usuario con el mismo nombre", "Aceptar");
            }
            else
            {
                bool resultValidate = validateEmail(Email);

                if (resultValidate)
                {
                    UserModel newUser = new UserModel();
                    if (Modify)
                        newUser.ID = User.ID;
                    newUser.UserName = UserName;
                    newUser.Password = Crypt.Encrypt(Password, "uah2019");
                    newUser.Email = Email;
                    newUser.Role = SelectedRole;

                    App.Database.SaveUser(newUser);
                    if (Modify)
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

        private ICommand _OnTapGestureRecognizerTappedViewPassword;
        public ICommand OnTapGestureRecognizerTappedViewPassword
        {
            get { return _OnTapGestureRecognizerTappedViewPassword ?? (_OnTapGestureRecognizerTappedViewPassword = new Command(() => OnTapGestureRecognizerTappedViewPasswordExecute())); }
        }

        private void OnTapGestureRecognizerTappedViewPasswordExecute()
        {
            if (ViewPassword)
            {
                ViewPassword = false;
                Eye = "eye.png";
            }
            else
            {
                ViewPassword = true;
                Eye = "noeye.png";
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
