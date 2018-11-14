using System;
using System.Collections.Generic;

using Xamarin.Forms;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Diagnostics;
using System.Threading;

namespace smartCubes.View.Activity
{
    public partial class PlayActivityView : ContentPage
    {
        public PlayActivityView()
        {
            InitializeComponent();
            Title = "Comenzar actividad";

            BindingContext = new PlayActivityView();
        }

    }
}
