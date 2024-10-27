using System.Globalization;
using WpfApp1.Enums;
using WpfApp1.Interfaces;
using WpfApp1.Models;

namespace ConsoleCalculator.Core.Services
{
    // Реализация вычислителя
    public class Evaluator : IEvaluator
    {
        private readonly Dictionary<string, IOperation> _operations;

        public Evaluator(IEnumerable<IOperation> operations)
        {
            _operations = operations.ToDictionary(op => op.GetOperator());
        }

        public double Evaluate(Node node)
        {
            // Если узел - число, вернуть его значение
            if (node.Token.Type == TokenType.Number)
            {
                if (!double.TryParse(node.Token.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double number))
                {
                    throw new ArgumentException($"Неверный формат числа '{node.Token.Value}'");
                }
                return number;
            }

            // Выполнение операции, представленной текущим узлом
            if (node.Token.Type == TokenType.Operator)
            {
                if (node.Left == null)
                {
                    throw new ArgumentException("Неверное выражение: потерян левый операнд");
                }
                if (node.Right == null)
                {
                    throw new ArgumentException("Неверное выражение: потерян правый операнд");
                }

                // Вычисление левого и правого подвыражения
                double left = Evaluate(node.Left);
                double right = Evaluate(node.Right);

                if (!_operations.ContainsKey(node.Token.Value))
                {
                    throw new ArgumentException($"Неожиданный оператор '{node.Token.Value}'");
                }

                // Проверка деления на ноль
                if (node.Token.Value == "/" && right == 0)
                {
                    throw new ArgumentException("Деление на ноль невозможно");
                }

                return _operations[node.Token.Value].Execute(left, right);
            }

            throw new ArgumentException($"Неожиданный токен '{node.Token.Value}'");
        }
    }

}
