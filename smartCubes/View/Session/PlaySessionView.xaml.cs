using System;
using smartCubes.Models;
using smartCubes.ViewModels.Session;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smartCubes.View.Session
{
    public partial class PlaySessionView : ContentPage
    {
        public PlaySessionView()
        {
            InitializeComponent();
        }

        public PlaySessionView(SessionModel session)
        {
            InitializeComponent();
            BindingContext = new PlaySessionViewModel(session);
        }


    }
}
