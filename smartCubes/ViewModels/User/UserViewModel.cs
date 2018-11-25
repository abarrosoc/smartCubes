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
            Title = "Usuarios";
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

        private async void DeleteCommandExecute(UserModel user)
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Desea elimina el usuario?", "Si","No");

            if(answer){
                App.Database.DeleteUser(user);
                await Application.Current.MainPage.DisplayAlert("Info", "El usuario se ha eliminado", "OK");
                RefreshData();
            }
        }

        private void RefreshData()
        {
            lUsers = new ObservableCollection<UserModel>();
            List<UserModel> listUsers = App.Database.GetUsers();

            foreach (UserModel user in listUsers)
                lUsers.Add(user);
        }
    }
}
