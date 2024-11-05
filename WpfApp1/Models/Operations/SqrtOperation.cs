using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    class SqrtOperation : IUnaryOperation
    {
        public string Operator => "sqrt";
        public double Execute(double number) => Math.Sqrt(number);
    }
}
