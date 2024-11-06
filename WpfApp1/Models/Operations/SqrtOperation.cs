using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    /// <summary>
    /// Операция возведения в корень числа
    /// </summary>
    class SqrtOperation : IUnaryOperation
    {
        public string GetOperator() => "sqrt";
        public double Execute(double number) => Math.Sqrt(number);
    }
}
