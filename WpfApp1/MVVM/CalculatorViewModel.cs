using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Services;
using WpfApp1.Utilities;

namespace WpfApp1.MVVM
{
    class CalculatorViewModel : Notifier
    {
        private readonly Calculator _calculatorService;

        public CalculatorViewModel(Calculator calculatorService)
        {
            _calculatorService = calculatorService;
            _expression = "0";
            _result = 0;
        }

        private string _expression;
        public string Expression
        {
            get { return _expression; }
            set
            {
                _expression = value;
                OnPropertyChanged();
            }
        }

        private double _result;
        public double Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        private ICommand _enterSymbolCommand;
        public ICommand EnterSymbolCommand
        {
            get
            {
                if (_enterSymbolCommand == null)
                {
                    _enterSymbolCommand = new RelayCommand(
                        param => AppendSymbol(param),
                        param => true);
                }
                return _enterSymbolCommand;
            }
        }

        private ICommand _calculateCommand;
        public ICommand CalculateCommand
        {
            get
            {
                if (_calculateCommand == null)
                {
                    _calculateCommand = new RelayCommand(
                        param => Calculate(_expression),
                        param => true);
                }
                return _calculateCommand;
            }
        }

        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new RelayCommand(
                        param => Clear(_expression),
                        param => true);
                }
                return _clearCommand;
            }
        }

        private ICommand _clearEntryCommand;
        public ICommand ClearEntryCommand
        {
            get
            {
                if (_clearEntryCommand == null)
                {
                    _clearEntryCommand = new RelayCommand(
                        param => ClearEntry(_expression),
                        param => true);
                }
                return _clearEntryCommand;
            }
        }

        private void AppendSymbol(object parameter)
        {
            if (Expression != "0")
                Expression += parameter;
            else Expression = parameter.ToString()!;
        }


        private void Calculate(object parameter)
        {
            try
            {
                Result = _calculatorService.Calculate(Expression);
                Expression = Result.ToString();
            }
            catch (Exception ex)
            {
                Expression=(ex.Message);
            }
        }

        private void Clear(object parameter)
        {
            Expression = "0";
        }

        private void ClearEntry(object parameter)
        {
            if (Expression != "0")
            {
            // Ищем последнее число в выражении
            Match match = Regex.Match(Expression, @"(\d+(\.\d+)?)$");
                if (match.Success)
                {
                    // Удаляем последнее число из строки
                    Expression = Expression.Substring(0, match.Index);
                }
            }
        }
        
    }
}
