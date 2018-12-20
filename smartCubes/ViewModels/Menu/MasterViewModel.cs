using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using smartCubes.Enum;
using smartCubes.Models;
using smartCubes.View.Activity;
using smartCubes.View.Session;
using smartCubes.View.User;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Menu
{
    public class MasterViewModel : BaseViewModel
    {
        public INavigation navigation { get; set; }

        private UserModel userLogin;

        public MasterViewModel(INavigation navigation,UserModel userLogin)
        {
            this.navigation = navigation;
            this.userLogin = userLogin;
            Title = "Menú";
                
            lMenu = new ObservableCollection<MasterPageItem>();

            lMenu.Add(new MasterPageItem
            {
                Title = "Sesiones",
                IconSource = "ble_settings.png",
                TargetType = typeof(SessionView)
            });
            lMenu.Add(new MasterPageItem
            {
                Title = "Usuarios",
                IconSource = "users.png",
                TargetType = typeof(UserView)
            });
            if (userLogin.Role.Equals(Role.Admin))
            {
                lMenu.Add(new MasterPageItem
                {
                    Title = "Actividades",
                    IconSource = "students.png",
                    TargetType = typeof(ActivityView)
                });
            }
        }

        private ObservableCollection<MasterPageItem> _lMenu;

        public ObservableCollection<MasterPageItem> lMenu
        {
            get
            {
                return _lMenu;
            }
            set
            {
                
                _lMenu = value;
                RaisePropertyChanged();
            }
        }

        private MasterPageItem _SelectedItem;

        public MasterPageItem SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _OnItemTapped;

        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(() => OnItemTappedExecute())); }
        }
        private void OnItemTappedExecute()
        {
            if (SelectedItem.TargetType == typeof(SessionView))
            {
                SessionView session = new SessionView(userLogin);
                App.MasterDetail.Detail.Navigation.PushAsync(session);
                SelectedItem = null;
                App.MasterDetail.IsPresented = false;
            }
            else if (SelectedItem.TargetType == typeof(UserView))
            {
                UserView users = new UserView(userLogin);
                App.MasterDetail.Detail.Navigation.PushAsync(users);
                App.MasterDetail.IsPresented = false;
            }
            else {
                App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(SelectedItem.TargetType));
                SelectedItem = null;
                App.MasterDetail.IsPresented = false;
            }
        }
    }
}

