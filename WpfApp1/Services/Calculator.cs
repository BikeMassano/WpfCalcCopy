using WpfApp1.Interfaces;
using WpfApp1.Models.Operations;

namespace WpfApp1.Services
{
    internal class Calculator : ICalculator
    {
        private readonly Dictionary<string, IBinaryOperation> _binaryOperations = new();
        private readonly Dictionary<string, IUnaryOperation> _unaryOperations = new();
        public Calculator()
        {
            // Добавление поддерживаемых операций
            _binaryOperations.Add("-", new SubtractOperation());
            _binaryOperations.Add("+", new AddOperation());
            _binaryOperations.Add("*", new MultiplyOperation());
            _binaryOperations.Add("/", new DivideOperation());
            _binaryOperations.Add("%", new ModulusOperation());
            _unaryOperations.Add("sqrt", new SqrtOperation());
            _unaryOperations.Add("pow", new PowerOperation());
            _unaryOperations.Add("1/x", new OneDivideXOperation());
        }

        public double Calculate(string expression)
        {
            var parts = expression.Split(' ');

            if (parts.Length == 2)  // Унарная операция
            {
                var operation = parts[0];
                var number = double.Parse(parts[1]);

                if (_unaryOperations.ContainsKey(operation))
                {
                    return _unaryOperations[operation].Execute(number);
                }
            }
            else if (parts.Length == 3)  // Бинарная операция
            {
                var left = double.Parse(parts[0]);
                var operation = parts[1];
                var right = double.Parse(parts[2]);

                if (_binaryOperations.ContainsKey(operation))
                {
                    return _binaryOperations[operation].Execute(left, right);
                }
            }

            throw new InvalidOperationException("Invalid expression");
        }
    }
}
