using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace smartCubes.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null) { 
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
             }
        }

        private string _Title;

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                RaisePropertyChanged("Title");
            }
        }
    }
}

