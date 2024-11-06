using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    /// <summary>
    /// Операция деления двух чисел
    /// </summary>
    public class DivideOperation : IBinaryOperation
    {
        public string GetOperator() => "/";
        public double Execute(double left, double right)
        {
            if (right == 0)
            {
                throw new ArgumentException("Деление на 0 невозможно");
            }

            return left / right;
        }
    }
}
