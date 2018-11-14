using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using smartCubes.View.Menu;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace smartCubes
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetail { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
           // MainPage = new NavigationPage(new ActivitiesView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
