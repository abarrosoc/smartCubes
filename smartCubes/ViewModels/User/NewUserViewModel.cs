﻿using System;
using System.Diagnostics;
using System.Windows.Input;
using smartCubes.Models;
using Xamarin.Forms;

namespace smartCubes.ViewModels.User
{
    public class NewUserViewModel : BaseViewModel
    {
        public NewUserViewModel()
        {
            Title = "Nuevo";
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

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new Command(() => SaveCommandExecuteAsync())); }
        }

        private async void SaveCommandExecuteAsync()
        {
            if(String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(Password)|| String.IsNullOrEmpty(Email)){
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar todos lo campos", "OK");
            }else{
                UserModel user = new UserModel();
                user.UserName = UserName;
                user.Password = Password;
                user.Email = Email;
                App.Database.SaveUser(user);

                await Application.Current.MainPage.DisplayAlert("Información", "El usuario se ha creado correctamente", "OK");

                UserName = "";
                Password = "";
                Email = "";
            }

        }
    }
}
