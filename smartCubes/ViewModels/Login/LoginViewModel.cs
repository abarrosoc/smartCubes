﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
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

        private String _User;

        public String User
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

        private String _Password;

        public String Password
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

            Loading = true;
            await Task.Run(() =>
            {

                if (App.Database.GetUsers().Count == 0)
                    App.Database.ResetDataBase();
                UserModel user = App.Database.GetUsers()[0];
                Application.Current.MainPage = new MainPage(user);
                /*
                //Application.Current.MainPage = new MainPage();
                if(User==null || Password==null){
                    Application.Current.MainPage.DisplayAlert("Login", "Debe rellenar todos los campos", "Aceptar");
                }else{

                    UserModel User = App.Database.GetUser(User);
                    if (User != null)
                    {
                        String userPass = Crypt.Decrypt(User.Password, "uah2019");
                        if (Password.Equals(userPass))
                        {
                            Application.Current.MainPage = new MainPage(User);
                        }
                        else
                        {
                            Application.Current.MainPage.DisplayAlert("Login", "Usuario o contraseña incorrecto", "Aceptar");
                        }
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Login", "Usuario o contraseña incorrecto", "Aceptar");
                    }

            }*/
            });
            Loading = false;
        }

    }
}
