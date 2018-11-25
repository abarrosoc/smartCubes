using System;
namespace smartCubes.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
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
    }
}
