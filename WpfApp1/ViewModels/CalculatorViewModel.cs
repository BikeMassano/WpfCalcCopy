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
        private readonly ICalculator _calculator = null!;

        private string _firstOperand = string.Empty;
        private string _secondOperand = DEFAULT_OPERAND_VALUE;

        private OperationModel? _currentOperation = new OperationModel();

        private const string DEFAULT_OPERAND_VALUE = "0";
        private const int MAX_OPERAND_LENGTH = 16;

        private bool _isOperationEnabled = true;
        private bool _canRepeatOperation = false;

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
            get => _isOperationEnabled;
            set => SetProperty(ref _isOperationEnabled, value);
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

        private ICommand _enterNumberCommand = null!;
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

        private ICommand _enterDotCommand = null!;
        public ICommand EnterDotCommand
        {
            get
            {
                if (_enterDotCommand == null)
                {
                    _enterDotCommand = new RelayCommand(
                        param => EnterDot(),
                        param => IsOperationEnabled);
                }
                return _enterDotCommand;
            }
        }

        private ICommand _enterBinaryOperatorCommand = null!;
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

        private ICommand _enterUnaryOperatorCommand = null!;
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

        private ICommand _equalCommand = null!;
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

        private ICommand _clearCommand = null!;
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

        private ICommand _invertCommand = null!;
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

        private ICommand _clearEntryCommand = null!;
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

        private ICommand _deleteSymbolCommand = null!;
        public ICommand DeleteSymbolCommand
        {
            get
            {
                if (_deleteSymbolCommand == null)
                {
                    _deleteSymbolCommand = new RelayCommand(
                        param => DeleteSymbol(),
                        param => true);
                }
                return _deleteSymbolCommand;
            }
        }

        private ICommand _clearLogsCommand = null!;
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
            if (IsOperationEnabled == false || _canRepeatOperation)
            {
                SecondOperand = parameter.ToString()!;
                _canRepeatOperation = false;
                IsOperationEnabled = true;
                return;
            }

            if (SecondOperand.Length < MAX_OPERAND_LENGTH)
            {

                if (SecondOperand != DEFAULT_OPERAND_VALUE)
                    SecondOperand += parameter;
                else
                    SecondOperand = parameter.ToString()!;
            }
        }

        public void EnterDot()
        {
            if (!SecondOperand.Contains(','))
                SecondOperand += ",";
        }

        public void OnBinaryOperatorButtonClicked(object parameter)
        {
            try
            {
                if (_canRepeatOperation)
                {
                    _canRepeatOperation = false;
                    FirstOperand = String.Empty;
                }
                // Если второй операнд не равен значению по умолчанию
                if (SecondOperand != DEFAULT_OPERAND_VALUE)
                {
                    // Если первый операнд еще не установлен
                    if (string.IsNullOrWhiteSpace(FirstOperand))
                    {
                        SetBinaryOperationState(SecondOperand, parameter.ToString());
                        return;
                    }

                    _currentOperation.SecondOperand = double.Parse(SecondOperand);
                    _currentOperation.FirstOperand = double.Parse(FirstOperand);

                    var expression = $"{_currentOperation.FirstOperand} {_currentOperation.Operation} {_currentOperation.SecondOperand}";

                    var result = _calculator.Calculate(expression);
                    SecondOperand = result.ToString();

                    AddLogToJournal(expression, result);
                    SetBinaryOperationState(SecondOperand, parameter.ToString());
                    return;
                }
            }
            catch(Exception)
            {
                IsOperationEnabled = false;
            }
        }

        private void SetBinaryOperationState(string secondOperand, string operation)
        {
            _currentOperation.FirstOperand = double.Parse(secondOperand);
            FirstOperand = secondOperand;
            _currentOperation.Operation = operation;
            SecondOperand = DEFAULT_OPERAND_VALUE;
            IsOperationEnabled = true;
        }

        public void OnUnaryOperationButtonClicked(object parameter)
        {
            try
            {
                FirstOperand = String.Empty;
                _canRepeatOperation = false;
                if (SecondOperand != DEFAULT_OPERAND_VALUE)
                {
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

        public void ExecuteUnaryOperation()
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
                if (!string.IsNullOrEmpty(FirstOperand))
                {
                    if(!_canRepeatOperation)
                    {
                        _currentOperation.SecondOperand = double.Parse(SecondOperand);
                        _currentOperation.FirstOperand = double.Parse(FirstOperand);

                        var expression = $"{_currentOperation.FirstOperand} {_currentOperation.Operation} {_currentOperation.SecondOperand}";

                        var result = _calculator.Calculate(expression);
                        SecondOperand = result.ToString();

                        AddLogToJournal(expression, result);
                        _canRepeatOperation = true;
                        return;
                    }

                    if (_canRepeatOperation)
                    {
                        _currentOperation.FirstOperand = double.Parse(SecondOperand);
                        FirstOperand = SecondOperand;

                        var expression = $"{_currentOperation.FirstOperand} {_currentOperation.Operation} {_currentOperation.SecondOperand}";

                        var result = _calculator.Calculate(expression);
                        SecondOperand = result.ToString();

                        AddLogToJournal(expression, result);
                        return;
                    }
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
            SecondOperand = DEFAULT_OPERAND_VALUE;
            IsOperationEnabled = true;
            _canRepeatOperation = false;
        }

        public void ClearEntry()
        {
            SecondOperand = DEFAULT_OPERAND_VALUE;
            IsOperationEnabled = true;
            _canRepeatOperation = false;
        }

        public void DeleteSymbol()
        {
            if (IsOperationEnabled == false)
            {
                Clear();
                IsOperationEnabled = true;
                return;
            }

            if (SecondOperand.Length > 1 && SecondOperand != DEFAULT_OPERAND_VALUE)
            {
                SecondOperand = SecondOperand.Substring(0, SecondOperand.Length - 1);
            }
            else
            {
                SecondOperand = DEFAULT_OPERAND_VALUE;
            }
        }

        public void InvertOperand()
        {
            if (SecondOperand != DEFAULT_OPERAND_VALUE)
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
