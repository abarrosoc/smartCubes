using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using smartCubes.View.Menu;
using smartCubes.Utils;
using smartCubes.Data;
using System;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace smartCubes
{
    public partial class App : Application
    {
        static Database database;

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
       

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
                }
                return database;
            }
        }
    }
}
