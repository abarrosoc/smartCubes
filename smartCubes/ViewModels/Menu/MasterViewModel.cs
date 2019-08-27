using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using smartCubes.Enum;
using smartCubes.Models;
using smartCubes.View.Activity;
using smartCubes.View.Login;
using smartCubes.View.Session;
using smartCubes.View.User;
using smartCubes.View.Configuration;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Menu
{
    public class MasterViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        private UserModel UserLogin;

        public MasterViewModel(INavigation navigation,UserModel userLogin)
        {
            Navigation = navigation;
            UserLogin = userLogin;
            Title = "Menú";
            Letter = userLogin.UserName.Substring(0, 1).ToUpper();    
            lMenu = new ObservableCollection<MasterPageItem>();
            Loading = false;

            lMenu.Add(new MasterPageItem
            {
                Title = "Sesiones",
                IconSource = "play.png",
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
                    IconSource = "activities.png",
                    TargetType = typeof(ActivityView)
                });

                lMenu.Add(new MasterPageItem
                {
                    Title = "Configuración",
                    IconSource = "settings.png",
                    TargetType = typeof(ConfigurationView)
                });
            }
            lMenu.Add(new MasterPageItem
            {
                Title = "Logout",
                IconSource = "exit.png",
                TargetType = typeof(LoginView)
            });
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
        private string _Letter;

        public string Letter
        {
            get
            {
                return _Letter;
            }
            set
            {
                _Letter = value;
                RaisePropertyChanged();
            }
        }
        private bool _Loading;
        public bool Loading
        {
            get
            {
                return _Loading;
            }
            set
            {
                _Loading = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _OnItemTapped;

        public ICommand OnItemTapped
        {
            get { return _OnItemTapped ?? (_OnItemTapped = new Command(() => OnItemTappedExecute())); }
        }
        private async void OnItemTappedExecute()
        {
            Loading = true;
            if (SelectedItem.TargetType == typeof(SessionView))
            {
                SessionView session = new SessionView(UserLogin);
                SelectedItem = null;
                await App.MasterDetail.Detail.Navigation.PushAsync(session);
                App.MasterDetail.IsPresented = false;
            }
            else if (SelectedItem.TargetType == typeof(UserView))
            {
                UserView users = new UserView(UserLogin);
                SelectedItem = null;
                await App.MasterDetail.Detail.Navigation.PushAsync(users);
                App.MasterDetail.IsPresented = false;
            }
            else if(SelectedItem.TargetType == typeof(LoginView)){
                Loading = false;
                var answer =  await Application.Current.MainPage.DisplayAlert("Cerrar sesión", "¿Está seguro de cerrar la sesión actual?", "Si", "No");

                if (answer)
                {
                    App.Current.MainPage = new LoginView();
                }

                SelectedItem = null;
            }
            else
            {
                await  App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(SelectedItem.TargetType));
                SelectedItem = null;
                App.MasterDetail.IsPresented = false;
            }
            Loading = false;
        }
    }
}

