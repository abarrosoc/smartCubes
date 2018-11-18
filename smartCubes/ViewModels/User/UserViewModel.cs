using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using smartCubes.Models;
using Xamarin.Forms;

namespace smartCubes.ViewModels.User
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel()
        {
            List<UserModel> listUsers = App.Database.GetUsers();
            lUsers = new ObservableCollection<UserModel>();

            foreach (UserModel user in listUsers)
                lUsers.Add(user);
        }
        private ObservableCollection<UserModel> _lUsers;

        public ObservableCollection<UserModel> lUsers
        {
            get
            {
                return _lUsers;
            }
            set
            {
                _lUsers = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new Command<UserModel>((user) => DeleteCommandExecute(user))); }
        }

        private void DeleteCommandExecute(UserModel user)
        {
            Debug.Write("Usuario: " + user.UserName);
                 App.Database.DeleteUser(user);

        }
    }
}
