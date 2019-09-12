using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using smartCubes.Enum;
using smartCubes.Models;
using smartCubes.View.User;
using Xamarin.Forms;

namespace smartCubes.ViewModels.User
{
    public class UserViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private UserModel UserLogin { get; set; }
        public UserViewModel(INavigation navigation, UserModel userLogin)
        {
            Navigation = navigation;
            UserLogin = userLogin;
            Title = "Usuarios";

            RefreshData();
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
            if (user.Role.Equals(Role.Admin) && !UserLogin.Role.Equals(Role.Admin)){
                await Application.Current.MainPage.DisplayAlert("Advertencia", "No tiene permisos para eliminar este usuario", "Aceptar");
            }
            else if(user.ID == UserLogin.ID)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "No puede eliminar su propio usuario", "Aceptar");
            }
            else
            {
                var answer = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Desea elimina el usuario?", "Si", "No");

                if (answer)
                {
                    App.Database.DeleteUser(user);
                    await Application.Current.MainPage.DisplayAlert("Información", "El usuario se ha eliminado", "Aceptar");
                    RefreshData();
                } 
            }
           
        }

        private UserModel _SelectItem;
        public UserModel SelectItem
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

        private ICommand _NewUserCommand;
        public ICommand NewUserCommand
        {
            get { return _NewUserCommand ?? (_NewUserCommand = new Command(() => NewUserCommandExecute())); }
        }

        private void NewUserCommandExecute()
        {
                Navigation.PushAsync(new UserFormView(Navigation, UserLogin, false, null));
        }

        private ICommand _OnItemTapped;
        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(() => OnItemTappedExecute())); }
        }

        private void OnItemTappedExecute()
        {
            if (SelectItem.Role.Equals(Role.Admin) && !UserLogin.Role.Equals(Role.Admin))
            {
                Application.Current.MainPage.DisplayAlert("Atención", "No tiene permisos para modificar este usuario", "Aceptar");
            }
            else
            {
                Navigation.PushAsync(new UserFormView(Navigation, UserLogin, true, SelectItem));
                SelectItem = null;
            }

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

        public void RefreshData()
        {
            lUsers = new ObservableCollection<UserModel>();
            List<UserModel> listUsers = App.Database.GetUsers();

            foreach (UserModel user in listUsers)
                lUsers.Add(user);

            if (lUsers.Count > 0)
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
