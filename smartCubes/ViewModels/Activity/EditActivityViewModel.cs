using System.Windows.Input;
using smartCubes.Models;
using smartCubes.Utils;
using smartCubes.View.Activity;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Activity
{
    public class EditActivityViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private bool Modify;
        private ActivityModel Activity;

        public EditActivityViewModel(INavigation navigation, bool modify, ActivityModel activity)
        {
            Navigation = navigation;
            Modify = modify;
            Activity = activity;

            if (modify)
            {
                Title = "Editar actividad";
                Name = activity.Name;
                Description = activity.Description;
            }
            else
            {
                Title = "Nueva actividad";
            }
        }

        private string _Name;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                RaisePropertyChanged();
            }
        }

        private string _Description;

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                RaisePropertyChanged();
            }
        }

        private string _ColorName;

        public string ColorName
        {
            get
            {
                return _ColorName;
            }
            set
            {
                _ColorName = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get { return _nextCommand ?? (_nextCommand = new Command(() => NextCommandExecute())); }
        }

        private async void NextCommandExecute()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe rellenar los campos obligatorios", "Aceptar");
            } else if (!Modify && Json.GetActivityByName(Name) != null)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Ya existe una actividad con el mismo nombre", "Aceptar");
            }
            else
            {
                if (Modify)
                {
                    Activity.Name = Name;
                    Activity.Description = Description;
                    await Navigation.PushAsync(new AddDeviceActivityView(Activity, Modify));
                }
                else
                {

                    ActivityModel newActivity = new ActivityModel();
                    newActivity.Name = Name;
                    newActivity.Description = Description;
                    await Navigation.PushAsync(new AddDeviceActivityView(newActivity, Modify));
                }
            }
        }
    }
}

