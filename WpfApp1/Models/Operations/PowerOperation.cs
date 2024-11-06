using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    /// <summary>
    /// Операция возведения в квадрат числа
    /// </summary>
    public class PowerOperation : IUnaryOperation
    {
        public string GetOperator() => "pow";
        public double Execute(double number) => Math.Pow(number, 2);
    }
}
