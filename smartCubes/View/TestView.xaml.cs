using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using smartCubes.ViewModels;
using Xamarin.Forms;

namespace smartCubes.View
{
    public partial class TestView : ContentPage
    {
        private TestViewModel _avm;

        public TestView()
        {
           
            InitializeComponent();
            //this.BindingContext = activitiesvm;

            _avm = new TestViewModel();
            BindingContext = _avm;
            //Task.Run(async () => await _avm.Init());
        }
    }
}
