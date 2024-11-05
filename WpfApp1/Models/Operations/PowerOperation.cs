using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    public class PowerOperation : IUnaryOperation
    {
        public string Operator => "pow";
        public double Execute(double number) => Math.Pow(number, 2);
    }
}
