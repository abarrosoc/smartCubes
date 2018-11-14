using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Menu
{
    public class MasterViewModel : BaseViewModel
    {
        public MasterViewModel()
        {
        }

        private List<MasterPageItem> _MasterPageItems;

        public List<MasterPageItem> MasterPageItems
        {
            get
            {
                return _MasterPageItems;
            }
            set
            {
                
                _MasterPageItems = value;
                RaisePropertyChanged();
            }
        }
    }
}

