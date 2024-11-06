using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    /// <summary>
    /// Операция вычитания двух чисел
    /// </summary>
    public class SubtractOperation : IBinaryOperation
    {
        public string GetOperator() => "-";
        public double Execute(double left, double right) => left - right;
    }
}
 