using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using smartCubes.Data;
using System;
using System.IO;
using smartCubes.View.Login;

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
            //MainPage = new MainPage();
            MainPage = new LoginView();
            //MainPage = new NavigationPage(new LoginView());
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
                //database.ResetDataBase();
                if (database == null)
                {
                    database = new Database(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DataBaseSQLite.db3"));
                }
                return database;
            }
        }
    }
}
