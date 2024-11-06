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
        private string _secondOperand = kDefaultOperandValue;

        private OperationModel? _currentOperation = new OperationModel();

        private const string kDefaultOperandValue = "0";
        private const int kMaxOperandLength = 16;

        private bool _isOperationDisabled = true;
        #endregion // Private members

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

        public bool IsOperationEnabled
        {
            get => _isOperationDisabled;
            set => SetProperty(ref _isOperationDisabled, value);
        }
        #endregion // Public Properties

        #region Constructors
        public CalculatorViewModel(ICalculator calculator)
        {
            _calculator = calculator;
            _currentOperation = new OperationModel();
            LogItems = new ObservableCollection<string>();
        }
        #endregion // Constructors

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

        private ICommand _enterDotCommand;
        public ICommand EnterDotCommand
        {
            get
            {
                if (_enterDotCommand == null)
                {
                    _enterDotCommand = new RelayCommand(
                        param => EnterDot(param),
                        param => IsOperationEnabled);
                }
                return _enterDotCommand;
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
                        param => IsOperationEnabled);
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
                        param => IsOperationEnabled);
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
                        param => IsOperationEnabled);
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
                        param => IsOperationEnabled);
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

        private ICommand _deleteSymbolCommand;
        public ICommand DeleteSymbolCommand
        {
            get
            {
                if (_deleteSymbolCommand == null)
                {
                    _deleteSymbolCommand = new RelayCommand(
                        param => DeleteSymbol(param),
                        param => IsOperationEnabled);
                }
                return _deleteSymbolCommand;
            }
        }

        private ICommand _clearLogsCommand;
        public ICommand ClearLogsCommand
        {
            get
            {
                if (_clearLogsCommand == null)
                {
                    _clearLogsCommand = new RelayCommand(
                        param => ClearLogsFromJournal(),
                        param => IsOperationEnabled);
                }
                return _clearLogsCommand;
            }
        }
        #endregion // Commands

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

        private void EnterDot(object parameter)
        {
            if (!SecondOperand.Contains(","))
                SecondOperand += ",";
        }

        public void OnBinaryOperatorButtonClicked(object parameter)
        {
            try
            {
                if (SecondOperand != kDefaultOperandValue)
                {
                    if (FirstOperand == String.Empty)
                    {
                        _currentOperation.FirstOperand = double.Parse(SecondOperand);
                        FirstOperand = SecondOperand;
                        _currentOperation.Operation = parameter.ToString();
                        SecondOperand = kDefaultOperandValue;
                        IsOperationEnabled = true;
                    }
                }
            }
            catch(Exception)
            {
                IsOperationEnabled = false;
            }
        }

        public void OnUnaryOperationButtonClicked(object parameter)
        {
            try
            {
                if (SecondOperand != kDefaultOperandValue && string.IsNullOrEmpty(FirstOperand))
                {
                    FirstOperand = String.Empty;
                    _currentOperation.FirstOperand = null;
                    _currentOperation.SecondOperand = double.Parse(SecondOperand);
                    _currentOperation.Operation = parameter.ToString();
                    ExecuteUnaryOperation();
                }
            }
            catch (Exception e)
            {
                Clear();
                SecondOperand = e.Message;
                IsOperationEnabled = false;
            }
        }

        private void ExecuteUnaryOperation()
        {
            try
            {
                var expression = $"{_currentOperation.Operation} {_currentOperation.SecondOperand}";
                var result = _calculator.Calculate(expression);
                SecondOperand = result.ToString();
                AddLogToJournal(expression, result);
                IsOperationEnabled = true;
            }
            catch (Exception e)
            {
                Clear();
                SecondOperand = e.Message;
                IsOperationEnabled = false;
            }
        }

        public void Equals()
        {
            try
            {
                if (FirstOperand != String.Empty)
                {
                    _currentOperation.SecondOperand = double.Parse(SecondOperand);
                    _currentOperation.FirstOperand = double.Parse(FirstOperand);

                    var expression = $"{_currentOperation.FirstOperand} {_currentOperation.Operation} {_currentOperation.SecondOperand}";

                    var result = _calculator.Calculate(expression);
                    SecondOperand = result.ToString();
                    FirstOperand = String.Empty;

                    AddLogToJournal(expression, result);
                }
            }
            catch (Exception e)
            {
                Clear();
                SecondOperand= e.Message;
                IsOperationEnabled = false;
            }
        }

        public void Clear()
        {
            _currentOperation = new OperationModel();
            FirstOperand = string.Empty;
            SecondOperand = kDefaultOperandValue;
            IsOperationEnabled = true;
        }

        public void ClearEntry()
        {
            SecondOperand = kDefaultOperandValue;
            IsOperationEnabled = true;
        }

        private void DeleteSymbol(object parameter)
        {
            if (SecondOperand.Length > 1 && SecondOperand != kDefaultOperandValue)
            {
                SecondOperand = SecondOperand.Substring(0, SecondOperand.Length - 1);
            }
            else
            {
                SecondOperand = kDefaultOperandValue;
            }
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
            LogItems.Insert(0, $"{expression} =\n{result}");
        }

        public void ClearLogsFromJournal()
        {
            LogItems.Clear();
        }
        #endregion // Methods

    }
}
