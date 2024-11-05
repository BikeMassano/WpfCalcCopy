using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp1.Interfaces;
using WpfApp1.Models;
using WpfApp1.Utilities;

namespace WpfApp1.ViewModels
{
    class CalculatorViewModel : Notifier
    {
        #region Private members
        private readonly ICalculator _calculator;
        private string _firstOperand = string.Empty;
        private string _secondOperand = "0";
        private OperationModel? _currentOperation = new OperationModel();
        private const string kDefaultOperandValue = "0";
        private const int kMaxOperandLength = 16;
        #endregion

        #region Public properties
        public ObservableCollection<string> LogItems { get; set; }
        public string FirstOperand
        {  
            get => _firstOperand; 
            set 
            { 
                _firstOperand = value;
                OnPropertyChanged();
            } 
        }

        public string SecondOperand
        {
            get => _secondOperand;
            set
            {
                _secondOperand = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public CalculatorViewModel(ICalculator calculator)
        {
            _calculator = calculator;
            _currentOperation = new OperationModel();
            LogItems = new ObservableCollection<string>();
        }
        #endregion

        #region Commands

        private ICommand _enterNumberCommand;
        public ICommand EnterNumberCommand
        {
            get
            {
                if (_enterNumberCommand == null)
                {
                    _enterNumberCommand = new RelayCommand(
                        param => OnNumberButtonClicked(param),
                        param => true);
                }
                return _enterNumberCommand;
            }
        }

        private ICommand _enterBinaryOperatorCommand;
        public ICommand EnterBinaryOperatorCommand
        {
            get
            {
                if (_enterBinaryOperatorCommand == null)
                {
                    _enterBinaryOperatorCommand = new RelayCommand(
                        param => OnBinaryOperatorButtonClicked(param),
                        param => true);
                }
                return _enterBinaryOperatorCommand;
            }
        }

        private ICommand _enterUnaryOperatorCommand;
        public ICommand EnterUnaryOperatorCommand
        {
            get
            {
                if (_enterUnaryOperatorCommand == null)
                {
                    _enterUnaryOperatorCommand = new RelayCommand(
                        param => OnUnaryOperationButtonClicked(param),
                        param => true);
                }
                return _enterUnaryOperatorCommand;
            }
        }

        private ICommand _equalCommand;
        public ICommand EqualsCommand
        {
            get
            {
                if (_equalCommand == null)
                {
                    _equalCommand = new RelayCommand(
                        param => Equals(),
                        param => true);
                }
                return _equalCommand;
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
                        param => Clear(),
                        param => true);
                }
                return _clearCommand;
            }
        }

        private ICommand _invertCommand;
        public ICommand InvertCommand
        {
            get
            {
                if (_invertCommand == null)
                {
                    _invertCommand = new RelayCommand(
                        param => InvertOperand(),
                        param => true);
                }
                return _invertCommand;
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
                        param => ClearEntry(),
                        param => true);
                }
                return _clearEntryCommand;
            }
        }
        #endregion

        #region Methods

        public void OnNumberButtonClicked(object parameter)
        {
            if (SecondOperand.Length < kMaxOperandLength)
            {
                if (SecondOperand != kDefaultOperandValue)
                    SecondOperand += parameter;
                else
                    SecondOperand = parameter.ToString()!;
            }
        }

        public void OnBinaryOperatorButtonClicked(object parameter)
        {
            if (SecondOperand != kDefaultOperandValue)
            {
                _currentOperation.FirstOperand = double.Parse(SecondOperand);
                FirstOperand = SecondOperand;
                _currentOperation.Operation = parameter.ToString();
                SecondOperand = kDefaultOperandValue;
            }
        }

        public void OnUnaryOperationButtonClicked(object parameter)
        {
            FirstOperand = String.Empty;
            _currentOperation.FirstOperand = null;
            _currentOperation.SecondOperand = double.Parse(SecondOperand);
            _currentOperation.Operation = parameter.ToString();
            var expression = $"{_currentOperation.Operation} {_currentOperation.SecondOperand}";
            var result = _calculator.Calculate(expression);
            SecondOperand = result.ToString();
            AddLogToJournal(expression, result);
        }

        public void Equals()
        {
            try
            {
                _currentOperation.SecondOperand = double.Parse(SecondOperand);
                _currentOperation.FirstOperand = double.Parse(FirstOperand);

                var expression = $"{_currentOperation.FirstOperand} {_currentOperation.Operation} {_currentOperation.SecondOperand}";

                var result = _calculator.Calculate(expression);
                SecondOperand = result.ToString();

                AddLogToJournal(expression, result);
            }
            catch (Exception e)
            {
                Clear();
                SecondOperand= e.Message;
            }
        }

        public void Clear()
        {
            _currentOperation = new OperationModel();
            FirstOperand = string.Empty;
            SecondOperand = kDefaultOperandValue;
        }

        public void ClearEntry()
        {
            SecondOperand = kDefaultOperandValue;
        }

        public void InvertOperand()
        {
            if (SecondOperand != kDefaultOperandValue)
            {
                if (double.TryParse(SecondOperand, out double operand))
                {
                    operand = -operand;
                    SecondOperand = operand.ToString();
                }
                else
                {
                    SecondOperand = "Ошибка";
                }
            }
        }
        
        public void AddLogToJournal(string expression, double result)
        {
            LogItems.Insert(0, $"{expression} = {result}");
        }
        #endregion

    }
}
