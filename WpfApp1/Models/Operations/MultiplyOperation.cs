using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    /// <summary>
    /// Операция умножения двух чисел
    /// </summary>
    class MultiplyOperation : IBinaryOperation
    {
        public string GetOperator() => "*";
        public double Execute(double left, double right) => left * right;
    }
}
