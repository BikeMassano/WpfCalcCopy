using WpfApp1.Interfaces;

namespace WpfApp1.Services
{
    class Calculator : ICalculator
    {
        private readonly Dictionary<string, IBinaryOperation> _binaryOperations = new();
        private readonly Dictionary<string, IUnaryOperation> _unaryOperations = new();
        public Calculator(IEnumerable<IBinaryOperation> binaryOperations, IEnumerable<IUnaryOperation> unaryOperations)
        {
            _binaryOperations = binaryOperations.ToDictionary(op => op.GetOperator());
            _unaryOperations = unaryOperations.ToDictionary(op => op.GetOperator());
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
