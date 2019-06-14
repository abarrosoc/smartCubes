using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using smartCubes.Models;
using Xamarin.Forms;

namespace smartCubes.ViewModels.Activity
{
    public class AddFieldsPopUpViewModel : BaseViewModel
    {
        public AddMessageActivityViewModel addMessageActivityViewModel { get; set; }

        private bool isModified { get; set; }

        public AddFieldsPopUpViewModel(AddMessageActivityViewModel addMessageActivityViewModel, bool isModified)
        {
            this.isModified = isModified;
            lSizes = new ObservableCollection<int>();
            for (int i = 0; i < 33; i++)
            {
                lSizes.Add(i);
            }

            this.addMessageActivityViewModel = addMessageActivityViewModel;

            if (addMessageActivityViewModel.FieldsTemp.Count == 15)
            {

                    SelectSize1 = addMessageActivityViewModel.FieldsTemp[0].Bytes;
                    SelectSize2 = addMessageActivityViewModel.FieldsTemp[1].Bytes;
                    SelectSize3 = addMessageActivityViewModel.FieldsTemp[2].Bytes;
                    SelectSize4 = addMessageActivityViewModel.FieldsTemp[3].Bytes;
                    SelectSize5 = addMessageActivityViewModel.FieldsTemp[4].Bytes;
                    SelectSize6 = addMessageActivityViewModel.FieldsTemp[5].Bytes;
                    SelectSize7 = addMessageActivityViewModel.FieldsTemp[6].Bytes;
                    SelectSize8 = addMessageActivityViewModel.FieldsTemp[7].Bytes;
                    SelectSize9 = addMessageActivityViewModel.FieldsTemp[8].Bytes;
                    SelectSize10 = addMessageActivityViewModel.FieldsTemp[9].Bytes;
                    SelectSize11 = addMessageActivityViewModel.FieldsTemp[10].Bytes;
                    SelectSize12 = addMessageActivityViewModel.FieldsTemp[11].Bytes;
                    SelectSize13 = addMessageActivityViewModel.FieldsTemp[12].Bytes;
                    SelectSize14 = addMessageActivityViewModel.FieldsTemp[13].Bytes;
                    SelectSize15 = addMessageActivityViewModel.FieldsTemp[14].Bytes;

                    Field1 = addMessageActivityViewModel.FieldsTemp[0].Description;
                    Field2 = addMessageActivityViewModel.FieldsTemp[1].Description;
                    Field3 = addMessageActivityViewModel.FieldsTemp[2].Description;
                    Field4 = addMessageActivityViewModel.FieldsTemp[3].Description;
                    Field5 = addMessageActivityViewModel.FieldsTemp[4].Description;
                    Field6 = addMessageActivityViewModel.FieldsTemp[5].Description;
                    Field7 = addMessageActivityViewModel.FieldsTemp[6].Description;
                    Field8 = addMessageActivityViewModel.FieldsTemp[7].Description;
                    Field9 = addMessageActivityViewModel.FieldsTemp[8].Description;
                    Field10 = addMessageActivityViewModel.FieldsTemp[9].Description;
                    Field11 = addMessageActivityViewModel.FieldsTemp[10].Description;
                    Field12 = addMessageActivityViewModel.FieldsTemp[11].Description;
                    Field13 = addMessageActivityViewModel.FieldsTemp[12].Description;
                    Field14 = addMessageActivityViewModel.FieldsTemp[13].Description;
                    Field15 = addMessageActivityViewModel.FieldsTemp[14].Description;

            }
        }
        private string _Field1;

        public string Field1
        {
            get
            {
                return _Field1;
            }
            set
            {
                _Field1 = value;
                RaisePropertyChanged();
            }
        }
        private string _Field2;

        public string Field2
        {
            get
            {
                return _Field2;
            }
            set
            {
                _Field2 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field3;

        public string Field3
        {
            get
            {
                return _Field3;
            }
            set
            {
                _Field3 = value;
                RaisePropertyChanged();
            }
        }
        private string _Field4;

        public string Field4
        {
            get
            {
                return _Field4;
            }
            set
            {
                _Field4 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field5;

        public string Field5
        {
            get
            {
                return _Field5;
            }
            set
            {
                _Field5 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field6;

        public string Field6
        {
            get
            {
                return _Field6;
            }
            set
            {
                _Field6 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field7;

        public string Field7
        {
            get
            {
                return _Field7;
            }
            set
            {
                _Field7 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field8;

        public string Field8
        {
            get
            {
                return _Field8;
            }
            set
            {
                _Field8 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field9;

        public string Field9
        {
            get
            {
                return _Field9;
            }
            set
            {
                _Field9 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field10;

        public string Field10
        {
            get
            {
                return _Field10;
            }
            set
            {
                _Field10 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field11;

        public string Field11
        {
            get
            {
                return _Field11;
            }
            set
            {
                _Field11 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field12;

        public string Field12
        {
            get
            {
                return _Field12;
            }
            set
            {
                _Field12 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field13;

        public string Field13
        {
            get
            {
                return _Field13;
            }
            set
            {
                _Field13 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field14;
        public string Field14
        {
            get
            {
                return _Field14;
            }
            set
            {
                _Field14 = value;
                RaisePropertyChanged();
            }
        }

        private string _Field15;

        public string Field15
        {
            get
            {
                return _Field15;
            }
            set
            {
                _Field15 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize1;

        public int SelectSize1
        {
            get
            {
                return _SelectSize1;
            }
            set
            {
                _SelectSize1 = value;
                RaisePropertyChanged();
            }
        }
        private int _SelectSize2;

        public int SelectSize2
        {
            get
            {
                return _SelectSize2;
            }
            set
            {
                _SelectSize2 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize3;

        public int SelectSize3
        {
            get
            {
                return _SelectSize3;
            }
            set
            {
                _SelectSize3 = value;
                RaisePropertyChanged();
            }
        }
        private int _SelectSize4;

        public int SelectSize4
        {
            get
            {
                return _SelectSize4;
            }
            set
            {
                _SelectSize4 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize5;

        public int SelectSize5
        {
            get
            {
                return _SelectSize5;
            }
            set
            {
                _SelectSize5 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize6;

        public int SelectSize6
        {
            get
            {
                return _SelectSize6;
            }
            set
            {
                _SelectSize6 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize7;

        public int SelectSize7
        {
            get
            {
                return _SelectSize7;
            }
            set
            {
                _SelectSize7 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize8;

        public int SelectSize8
        {
            get
            {
                return _SelectSize8;
            }
            set
            {
                _SelectSize8 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize9;

        public int SelectSize9
        {
            get
            {
                return _SelectSize9;
            }
            set
            {
                _SelectSize9 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize10;

        public int SelectSize10
        {
            get
            {
                return _SelectSize10;
            }
            set
            {
                _SelectSize10 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize11;

        public int SelectSize11
        {
            get
            {
                return _SelectSize11;
            }
            set
            {
                _SelectSize11 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize12;

        public int SelectSize12
        {
            get
            {
                return _SelectSize12;
            }
            set
            {
                _SelectSize12 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize13;

        public int SelectSize13
        {
            get
            {
                return _SelectSize13;
            }
            set
            {
                _SelectSize13 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize14;
        public int SelectSize14
        {
            get
            {
                return _SelectSize14;
            }
            set
            {
                _SelectSize14 = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectSize15;

        public int SelectSize15
        {
            get
            {
                return _SelectSize15;
            }
            set
            {
                _SelectSize15 = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<int> _lSizes;

        public ObservableCollection<int> lSizes
        {
            get
            {
                return _lSizes;
            }
            set
            {
                _lSizes = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _AddCommand;

        public ICommand AddCommand
        {
            get { return _AddCommand ?? (_AddCommand = new Command(() => AddCommandExecute())); }
        }
        private async void AddCommandExecute()
        {
            addMessageActivityViewModel.FieldsTemp = new List<FieldMessage>();
            FieldMessage fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize1;
            fieldTemp.Description = Field1;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize2;
            fieldTemp.Description = Field2;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize3;
            fieldTemp.Description = Field3;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize4;
            fieldTemp.Description = Field4;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize5;
            fieldTemp.Description = Field5;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize6;
            fieldTemp.Description = Field6;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize7;
            fieldTemp.Description = Field7;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize8;
            fieldTemp.Description = Field8;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize9;
            fieldTemp.Description = Field9;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize10;
            fieldTemp.Description = Field10;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize11;
            fieldTemp.Description = Field11;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize12;
            fieldTemp.Description = Field12;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize13;
            fieldTemp.Description = Field13;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize14;
            fieldTemp.Description = Field14;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize15;
            fieldTemp.Description = Field15;
            addMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            int totalSize = 0;
            foreach(FieldMessage fieldSize in addMessageActivityViewModel.FieldsTemp)
            {
                totalSize += fieldSize.Bytes;
            }

            addMessageActivityViewModel.Size = totalSize.ToString();

            await PopupNavigation.Instance.PopAsync();
        }

        private ICommand _cancelFieldCommand;

        public ICommand CancelCommand
        {
            get { return _cancelFieldCommand ?? (_cancelFieldCommand = new Command(() => CancelCommandExecute())); }
        }

        private async void CancelCommandExecute()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
