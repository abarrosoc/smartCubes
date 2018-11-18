using System;
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

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new Command(() => SaveCommandExecute())); }
        }

        private void SaveCommandExecute()
        {
            UserModel user = new UserModel();
            user.UserName = UserName;
            user.Password = Password;
  
            App.Database.SaveUser(user);


        }
    }
}
