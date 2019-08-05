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
        public AddMessageActivityViewModel AddMessageActivityViewModel { get; set; }

        private bool IsModified { get; set; }

        public AddFieldsPopUpViewModel(AddMessageActivityViewModel addMessageActivityViewModel, bool isModified)
        {
            IsModified = isModified;
            lSizes = new ObservableCollection<int>();
            for (int i = 0; i < 33; i++)
            {
                lSizes.Add(i);
            }

            lFormats = new ObservableCollection<string>
            {
                "",
                "Tiempo",
                "Fecha"
            };

            this.AddMessageActivityViewModel = addMessageActivityViewModel;

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

                    SelectFormat1 = addMessageActivityViewModel.FieldsTemp[0].Format;
                    SelectFormat2 = addMessageActivityViewModel.FieldsTemp[1].Format;
                    SelectFormat3 = addMessageActivityViewModel.FieldsTemp[2].Format;
                    SelectFormat4 = addMessageActivityViewModel.FieldsTemp[3].Format;
                    SelectFormat5 = addMessageActivityViewModel.FieldsTemp[4].Format;
                    SelectFormat6 = addMessageActivityViewModel.FieldsTemp[5].Format;
                    SelectFormat7 = addMessageActivityViewModel.FieldsTemp[6].Format;
                    SelectFormat8 = addMessageActivityViewModel.FieldsTemp[7].Format;
                    SelectFormat9 = addMessageActivityViewModel.FieldsTemp[8].Format;
                    SelectFormat10 = addMessageActivityViewModel.FieldsTemp[9].Format;
                    SelectFormat11 = addMessageActivityViewModel.FieldsTemp[10].Format;
                    SelectFormat12 = addMessageActivityViewModel.FieldsTemp[11].Format;
                    SelectFormat13 = addMessageActivityViewModel.FieldsTemp[12].Format;
                    SelectFormat14 = addMessageActivityViewModel.FieldsTemp[13].Format;
                    SelectFormat15 = addMessageActivityViewModel.FieldsTemp[14].Format;

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

        private string _SelectFormat1;

        public string SelectFormat1
        {
            get
            {
                return _SelectFormat1;
            }
            set
            {
                _SelectFormat1 = value;
                RaisePropertyChanged();
            }
        }
        private string _SelectFormat2;

        public string SelectFormat2
        {
            get
            {
                return _SelectFormat2;
            }
            set
            {
                _SelectFormat2 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat3;

        public string SelectFormat3
        {
            get
            {
                return _SelectFormat3;
            }
            set
            {
                _SelectFormat3 = value;
                RaisePropertyChanged();
            }
        }
        private string _SelectFormat4;

        public string SelectFormat4
        {
            get
            {
                return _SelectFormat4;
            }
            set
            {
                _SelectFormat4 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat5;

        public string SelectFormat5
        {
            get
            {
                return _SelectFormat5;
            }
            set
            {
                _SelectFormat5 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat6;

        public string SelectFormat6
        {
            get
            {
                return _SelectFormat6;
            }
            set
            {
                _SelectFormat6 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat7;

        public string SelectFormat7
        {
            get
            {
                return _SelectFormat7;
            }
            set
            {
                _SelectFormat7 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat8;

        public string SelectFormat8
        {
            get
            {
                return _SelectFormat8;
            }
            set
            {
                _SelectFormat8 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat9;

        public string SelectFormat9
        {
            get
            {
                return _SelectFormat9;
            }
            set
            {
                _SelectFormat9 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat10;

        public string SelectFormat10
        {
            get
            {
                return _SelectFormat10;
            }
            set
            {
                _SelectFormat10 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat11;

        public string SelectFormat11
        {
            get
            {
                return _SelectFormat11;
            }
            set
            {
                _SelectFormat11 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat12;

        public string SelectFormat12
        {
            get
            {
                return _SelectFormat12;
            }
            set
            {
                _SelectFormat12 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat13;

        public string SelectFormat13
        {
            get
            {
                return _SelectFormat13;
            }
            set
            {
                _SelectFormat13 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat14;
        public string SelectFormat14
        {
            get
            {
                return _SelectFormat14;
            }
            set
            {
                _SelectFormat14 = value;
                RaisePropertyChanged();
            }
        }

        private string _SelectFormat15;

        public string SelectFormat15
        {
            get
            {
                return _SelectFormat15;
            }
            set
            {
                _SelectFormat15 = value;
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
        private ObservableCollection<string> _lFormats;

        public ObservableCollection<string> lFormats
        {
            get
            {
                return _lFormats;
            }
            set
            {
                _lFormats = value;
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
            AddMessageActivityViewModel.FieldsTemp = new List<FieldMessage>();
            FieldMessage fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize1;
            fieldTemp.Description = Field1;
            fieldTemp.Format = SelectFormat1;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize2;
            fieldTemp.Description = Field2;
            fieldTemp.Format = SelectFormat2;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize3;
            fieldTemp.Description = Field3;
            fieldTemp.Format = SelectFormat3;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize4;
            fieldTemp.Description = Field4;
            fieldTemp.Format = SelectFormat4;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize5;
            fieldTemp.Description = Field5;
            fieldTemp.Format = SelectFormat5;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize6;
            fieldTemp.Description = Field6;
            fieldTemp.Format = SelectFormat6;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize7;
            fieldTemp.Description = Field7;
            fieldTemp.Format = SelectFormat7;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize8;
            fieldTemp.Description = Field8;
            fieldTemp.Format = SelectFormat8;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize9;
            fieldTemp.Description = Field9;
            fieldTemp.Format = SelectFormat9;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize10;
            fieldTemp.Description = Field10;
            fieldTemp.Format = SelectFormat10;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize11;
            fieldTemp.Description = Field11;
            fieldTemp.Format = SelectFormat11;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize12;
            fieldTemp.Description = Field12;
            fieldTemp.Format = SelectFormat12;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize13;
            fieldTemp.Description = Field13;
            fieldTemp.Format = SelectFormat13;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize14;
            fieldTemp.Description = Field14;
            fieldTemp.Format = SelectFormat14;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            fieldTemp = new FieldMessage();
            fieldTemp.Bytes = SelectSize15;
            fieldTemp.Description = Field15;
            fieldTemp.Format = SelectFormat15;
            AddMessageActivityViewModel.FieldsTemp.Add(fieldTemp);

            int totalSize = 0;
            foreach(FieldMessage fieldSize in AddMessageActivityViewModel.FieldsTemp)
            {
                totalSize += fieldSize.Bytes;
            }

            if (totalSize >= 20)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "El tamaño máximo es de 20 bytes", "Aceptar");
            }

            AddMessageActivityViewModel.Size = totalSize.ToString();
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
