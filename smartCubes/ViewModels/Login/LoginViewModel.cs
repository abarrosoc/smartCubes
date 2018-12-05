using System;
using System.Windows.Input;
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
        public static MasterDetailPage MasterDetail { get; set; }
        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get { return _LoginCommand ?? (_LoginCommand = new Command(() => LoginCommandExecuteAsync())); }
        }

        private void LoginCommandExecuteAsync()
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}
