using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Utilities;

namespace WpfApp1.MVVM
{
    class CalculatorViewModel : ViewModelBase
    {
        private CalculatorModel _calc;

        public RelayCommand NumberCommand { get; private set; }
        public RelayCommand PlusCommand { get; private set; }
        public RelayCommand MinusCommand { get; private set; }
        public RelayCommand TimesCommand { get; private set; }
        public RelayCommand OverCommand { get; private set; }
        public RelayCommand EqualsCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public String Display
        {
            get
            {
                return _calc.Display;
            }
        }

        public CalculatorViewModel(CalculatorModel calculator)
        {
            _calc = calculator;

            NumberCommand = new RelayCommand(param => { _calc.Number(Convert.ToInt32(param)); UpdateDisplay(); },
                                            param => _calc.CanDoNumber());

            PlusCommand = new RelayCommand(param => { _calc.Plus(); UpdateDisplay(); },
                                            param => _calc.CanDoOperator());

            MinusCommand = new RelayCommand(param => { _calc.Minus(); UpdateDisplay(); },
                                            param => _calc.CanDoOperator());

            TimesCommand = new RelayCommand(param => { _calc.Times(); UpdateDisplay(); },
                                            param => _calc.CanDoOperator());

            OverCommand = new RelayCommand(param => { _calc.Over(); UpdateDisplay(); },
                                            param => _calc.CanDoOperator());

            EqualsCommand = new RelayCommand(param => { _calc.Equals(); UpdateDisplay(); },
                                            param => _calc.CanDoEquals());

            ClearCommand = new RelayCommand(param => { _calc.Clear(); UpdateDisplay(); },
                                            param => _calc.CanDoClear());
        }

        private void UpdateDisplay()
        {
            OnPropertyChanged("Display");
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
