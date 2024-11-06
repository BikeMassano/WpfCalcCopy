using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    /// <summary>
    /// Операция сложения двух чисел
    /// </summary>
    class AddOperation : IBinaryOperation
    {
        public string GetOperator() => "+";

        public double Execute(double left, double right) => left + right;
    }
}
