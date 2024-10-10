namespace WpfApp1.Models
{
    public class CalculatorModel
    {
        private decimal? _operand1;
        private char? _operator;
        private decimal? _operand2;
        private decimal? _result;
        private String _display;

        public decimal? Result
        {
            get
            {
                if (!_result.HasValue)
                {
                    throw new InvalidOperationException();
                }
                return _result;
            }
        }

        public String Display
        {
            get
            {
                return _display;
            }
        }

        public CalculatorModel()
        {
            Clear();
        }

        public bool CanDoOperator()
        {
            return _operand1.HasValue && !_operator.HasValue;
        }

        public void Plus()
        {
            DoOperator('+');
        }

        public void Minus()
        {
            DoOperator('-');
        }

        public void Times()
        {
            DoOperator('*');
        }

        public void Over()
        {
            DoOperator('/');
        }

        private void DoOperator(char operation)
        {
            if (!CanDoOperator())
            {
                throw new InvalidOperationException();
            }

            _operator = operation;
            _display += " " + operation + " ";
        }

        public bool CanDoNumber()
        {
            return !_result.HasValue;
        }

        public void Number(int num)
        {
            if (!CanDoNumber())
            {
                throw new InvalidOperationException();
            }

            if ((num < 0) || (num > 9))
            {
                throw new ArgumentException();
            }

            if (!_operator.HasValue)
            {
                if (!_operand1.HasValue)
                {
                    _operand1 = num;
                    _display = num.ToString();
                }
                else
                {
                    _operand1 = _operand1 * 10 + num;
                    _display += num;
                }
            }
            else
            {
                if (!_operand2.HasValue)
                {
                    _operand2 = num;
                }
                else
                {
                    _operand2 = _operand2 * 10 + num;
                }

                _display += num;
            }
        }

        public bool CanDoEquals()
        {
            return (_operand1.HasValue && _operator.HasValue && _operand2.HasValue && !_result.HasValue);
        }

        public void Equals()
        {
            if (!CanDoEquals())
            {
                throw new InvalidOperationException();
            }

            switch (_operator)
            {
                case '+':
                    _result = _operand1 + _operand2;
                    break;
                case '-':
                    _result = _operand1 - _operand2;
                    break;
                case '*':
                    _result = _operand1 * _operand2;
                    break;
                case '/':
                    try
                    {
                        _result = _operand1 / _operand2;
                    }
                    catch (Exception ex)
                    {
                        _display = ("Деление на ноль невозможно");
                        return;
                    }
                    break;
            }

            _display = _result.ToString();
        }

        public bool CanDoClear()
        {
            return true;
        }

        public void Clear()
        {
            _operand1 = null;
            _operator = null;
            _operand2 = null;
            _result = null;
            _display = "0";
        }
    }
}
